
# 🧾 CSV Inconsistencies Summary

## 📁 1. Customer Orders - Raw

| Type of Inconsistency         | Description                                                                 |
|------------------------------|-----------------------------------------------------------------------------|
| **Inconsistent casing**      | `Customer_Name`, `Currency`, and `Country` values vary: "usa", "UsA", "USD", etc. |
| **Duplicate entries**        | Duplicate order for `JOHN DOE` with same email and amount.                  |
| **Date format issues**       | Various formats used: `2024-11-04`, `11/06/2024`, `07-11-2024`, `8th Nov 2024` |
| **Currency formatting**      | Some values include `$` symbol: e.g., `$120.00` vs. `180.00`                |
| **Trailing spaces**          | Columns like `Amount ` have extra spaces                                     |
| **Missing values**           | Missing `Customer_Name` or `Email` fields                                    |

---

## 🧾 2. Employee Records - Raw

| Type of Inconsistency         | Description                                                                 |
|------------------------------|-----------------------------------------------------------------------------|
| **Inconsistent date formats**| Dates like `1992/02/14`, `12-30-1985`, `01/06/2020`, `15-08-2021`            |
| **Salary format issues**     | `"60,000"` uses comma; others don't; some have extra spaces; one is missing |
| **Duplicate records**        | Employee ID `1002` appears twice with same data                             |
| **Whitespace and casing**    | Irregular casing/spacing in `Department`, `Status` (e.g., `Sales ` vs `Sales`) |
| **Missing values**           | Missing `Department` and `Salary` for some records                          |
| **Status casing**            | Variations: `Active`, `active`, `InActive`, `ACTIVE`                        |

---

## 📦 3. Product Inventory - Raw

| Type of Inconsistency         | Description                                                                 |
|------------------------------|-----------------------------------------------------------------------------|
| **Casing issues**            | Inconsistent `product_name` and `category` (e.g., `widget b`, `hardware`)   |
| **Missing values**           | Missing `product_name` and `Quantity` in some records                       |
| **Unit price issues**        | Non-numeric value `twenty`, use of `$` symbol inconsistently                |
| **Date format inconsistency**| Mixed styles: `2024-10-01`, `10/03/2024`, `March 25 2024`, `2024/03/11`     |
| **Duplicate products**       | `Product_ID` `P002` appears twice                                           |
| **Extra spaces**             | Fields have trailing or leading spaces                                      |
