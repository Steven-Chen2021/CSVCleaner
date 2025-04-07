# CSV Cleanup RESTful API Specification

## 📌 Overview

This API allows businesses to upload unstructured CSV files and returns a cleaned, standardized version. The API detects and corrects inconsistencies such as missing headers, irregular delimiters, whitespace issues, date/number format normalization, and more.

---

## 🛠️ Technology Stack

- **Backend**: ASP.NET Core Web API
- **Language**: C#
- **Framework**: .NET Core 6 or later
- **File Format**: CSV


---

## 📁 API Endpoints

### 1. `POST /api/csv/clean`

Upload and clean a CSV file.

#### 🔹 Request

- **Content-Type**: `multipart/form-data`
- **Form Field**: `file` (CSV file)
- **Optional Query Parameters**:
  - `delimiter` (e.g. `,`, `;`, `\t`) - default: `,`
  - `hasHeader` (boolean) - default: `true`

#### 📥 Example Request

```bash
curl -X POST https://yourapi.com/api/csv/clean \
  -H "Content-Type: multipart/form-data" \
  -F "file=@data.csv"
```

#### 🔹 Response

- **Status Code**: `200 OK`
- **Content-Type**: `application/json` or `text/csv` (based on `Accept` header)

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

## 🔄 Processing Features

| Feature                  | Description                                                             |
|--------------------------|-------------------------------------------------------------------------|
| Header normalization     | Capitalizes, trims, and standardizes column names                       |
| Empty row removal        | Deletes completely blank rows                                           |
| Whitespace trimming      | Trims spaces around values                                              |
| Data type standardization| Normalizes dates (`yyyy-MM-dd`), numbers (no currency symbols), etc.    |
| Invalid value correction | (Optional) Auto-correct common issues (e.g. fix email format)           |
| Logging/reporting        | Returns a summary of what was fixed                                     |

---

## 🧪 Future Enhancements

- Preview cleaned data in UI before download
- Column mapping with user-defined schema
- Webhook or callback support for async processing
- Download cleaned CSV directly in browser

---

## 🧾 Example Input/Output

### Original CSV (Partial)

```csv
Customer ID, Name , Date of Birth , Email
001, John Doe , 1/2/1990 , johndoe(at)email.com
002,   Jane Smith,02-15-1992,jane.smith@email.com
,,,
```

### Cleaned Output

```csv
CustomerID,Name,DateOfBirth,Email
001,John Doe,1990-01-02,johndoe@email.com
002,Jane Smith,1992-02-15,jane.smith@email.com
```

---

## 🔐 Security & Validation

- Limit file size (e.g., max 5MB)
- Validate file format (must be `.csv`)
- Sanitize file name & content


