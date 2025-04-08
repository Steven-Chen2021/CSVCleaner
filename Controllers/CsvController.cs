using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace CSVCleanApiProject.Controllers;

[ApiController]
[Route("api/csv")]
public class CsvController : ControllerBase
{
    [HttpGet("test")]
    public IActionResult TestConnection()
    {
        return Ok(new { message = "CSV Cleanup API is alive and running!" });
    }

    [HttpPost("clean")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> CleanCsvFile([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("未上傳檔案或檔案為空。");
        }

        try
        {
            using var stream = file.OpenReadStream();
            using var reader = new StreamReader(stream);
            using var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                TrimOptions = TrimOptions.Trim,
                IgnoreBlankLines = true
            });

            var records = csvReader.GetRecords<dynamic>().ToList(); // 讀取 CSV 資料為動態物件
            var cleanedRecords = new List<dynamic>();
            int blankRowsRemoved = 0, invalidEmailsCorrected = 0, extraWhitespaceTrimmed = 0;
            int inconsistentCasingFixed = 0, duplicateEntriesRemoved = 0, dateFormatsStandardized = 0, currencyFormatsStandardized = 0;

            var seenRecords = new HashSet<string>(); // 用於檢測重複記錄

            foreach (var record in records)
            {
                var row = record as IDictionary<string, object>;
                if (row == null || row.Values.All(value => string.IsNullOrWhiteSpace(value?.ToString())))
                {
                    blankRowsRemoved++;
                    continue;
                }

                // 修正不一致的大小寫
                foreach (var key in row.Keys.ToList())
                {
                    if (row[key] is string value)
                    {
                        row[key] = value.Trim();
                        if (key == "Customer_Name" || key == "Currency" || key == "Country")
                        {
                            row[key] = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(value.ToLower());
                            inconsistentCasingFixed++;
                        }
                    }
                }

                // 修正電子郵件格式
                if (row.ContainsKey("Email") && row["Email"] is string email)
                {
                    if (!email.Contains("@") || !email.Contains("."))
                    {
                        row["Email"] = $"{email}@example.com"; // 假設修正
                        invalidEmailsCorrected++;
                    }
                }

                // 標準化日期格式
                if (row.ContainsKey("Date") && row["Date"] is string dateValue)
                {
                    if (DateTime.TryParse(dateValue, out var parsedDate))
                    {
                        row["Date"] = parsedDate.ToString("yyyy-MM-dd");
                        dateFormatsStandardized++;
                    }
                }

                // 標準化貨幣格式
                if (row.ContainsKey("Amount") && row["Amount"] is string amountValue)
                {
                    row["Amount"] = amountValue.Replace("$", "").Trim();
                    if (decimal.TryParse(row["Amount"].ToString(), out var parsedAmount))
                    {
                        row["Amount"] = parsedAmount.ToString("F2");
                        currencyFormatsStandardized++;
                    }
                }

                // 移除重複記錄
                var rowSignature = string.Join("|", row.Values);
                if (seenRecords.Contains(rowSignature))
                {
                    duplicateEntriesRemoved++;
                    continue;
                }
                seenRecords.Add(rowSignature);

                cleanedRecords.Add(row);
            }

            // 生成清理後的 CSV
            using var outputStream = new MemoryStream();
            using var writer = new StreamWriter(outputStream);
            using var csvWriter = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));

            csvWriter.WriteRecords(cleanedRecords);
            writer.Flush();
            outputStream.Position = 0;

            // 模擬生成下載 URL
            var downloadUrl = $"https://yourapi.com/downloads/cleaned-{Guid.NewGuid()}.csv";

            var response = new
            {
                originalRowCount = records.Count,
                cleanedRowCount = cleanedRecords.Count,
                corrections = new
                {
                    blankRowsRemoved,
                    invalidEmailsCorrected,
                    extraWhitespaceTrimmed,
                    inconsistentCasingFixed,
                    duplicateEntriesRemoved,
                    dateFormatsStandardized,
                    currencyFormatsStandardized
                },
                downloadUrl
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"處理檔案時發生錯誤: {ex.Message}");
        }
    }
}