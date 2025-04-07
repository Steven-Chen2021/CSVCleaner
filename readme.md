# CSVCleanApiProject

## 📌 專案簡介

CSVCleanApiProject 是一個基於 ASP.NET Core 的 RESTful API，專為清理和標準化 CSV 文件而設計。此 API 可幫助企業處理未結構化的 CSV 文件，並返回清理後的標準化版本，解決如缺失標題、不規則分隔符、空白問題、日期/數字格式不一致等問題。

---

## 🛠️ 技術棧

- **後端**: ASP.NET Core Web API
- **語言**: C#
- **框架**: .NET 9.0
- **文件格式**: CSV

---

## 📁 API 端點

### 1. `POST /api/csv/clean`

上傳並清理 CSV 文件。

#### 🔹 請求

- **Content-Type**: `multipart/form-data`
- **表單欄位**: `file` (CSV 文件)
- **可選查詢參數**:
  - `delimiter` (例如 `,`, `;`, `\t`) - 預設值: `,`
  - `hasHeader` (布林值) - 預設值: `true`

#### 📥 範例請求

```bash
curl -X POST http://localhost:5171/api/csv/clean \
  -H "Content-Type: multipart/form-data" \
  -F "file=@data.csv"
```

#### 🔹 回應

- **狀態碼**: `200 OK`
- **Content-Type**: `application/json` 或 `text/csv` (根據 `Accept` 標頭)

```json
{
  "originalRowCount": 150,
  "cleanedRowCount": 147,
  "standardizedHeaders": ["CustomerID", "Name", "DateOfBirth", "Email"],
  "corrections": {
    "blankRowsRemoved": 3,
    "invalidEmailsCorrected": 2,
    "extraWhitespaceTrimmed": 147,
    "dateFormatStandardized": "yyyy-MM-dd"
  },
  "downloadUrl": "https://yourapi.com/downloads/cleaned-abc123.csv"
}
```

---

## 🔄 處理功能

| 功能                     | 描述                                                                 |
|--------------------------|--------------------------------------------------------------------|
| 標題標準化               | 將欄位名稱大寫化、修剪空白並標準化                                  |
| 空行移除                 | 刪除完全空白的行                                                   |
| 空白修剪                 | 修剪值周圍的空白                                                   |
| 資料類型標準化           | 標準化日期（`yyyy-MM-dd`）、數字（無貨幣符號）等                     |
| 無效值修正               | （可選）自動修正常見問題（例如修正電子郵件格式）                     |
| 日誌/報告                | 返回修正內容的摘要                                                 |

---

## 🧪 未來增強功能

- 在下載前於 UI 中預覽清理後的數據
- 使用者自定義結構的欄位映射
- 支援非同步處理的 Webhook 或回調
- 直接在瀏覽器中下載清理後的 CSV

---

## 🔐 安全性與驗證

- 限制文件大小（例如，最大 5MB）
- 驗證文件格式（必須為 `.csv`）
- 清理文件名稱與內容

---

## 🚀 如何執行專案

### 1. 安裝相依套件

執行以下命令以安裝專案所需的相依套件：

```bash
dotnet restore
```

### 2. 執行專案

使用以下命令啟動專案：

```bash
dotnet run
```

預設應用程式將在 `http://localhost:5171` 或 `https://localhost:7089` 上運行。

---

## 📂 專案結構

```
CSVCleanApiProject/
├── Controllers/
│   ├── CsvController.cs
│   ├── SwaggerFileOperationFilter.cs
│   ├── WeatherForecastController.cs
├── docs/
│   ├── ConventionalCommit.md
│   ├── spec.md
├── Properties/
│   ├── launchSettings.json
├── Program.cs
├── appsettings.json
├── appsettings.Development.json
├── CSVCleanApiProject.csproj
└── README.md
```

---

## 📜 授權

此專案採用 MIT 授權條款。詳細內容請參閱 [LICENSE](LICENSE) 文件。