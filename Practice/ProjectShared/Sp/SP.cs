using System.Diagnostics.Metrics;
//using static System.Runtime.InteropServices.JavaScript.JSType;

//USE[Practice]
//GO
///****** Object:  StoredProcedure [dbo].[GetMedicineDetails]    Script Date: 11/4/2025 10:11:47 PM ******/
//SET ANSI_NULLS ON
//GO
//SET QUOTED_IDENTIFIER ON
//GO
//-- =============================================
//-- Author:		< Dilawar Bukhari,, Name>
//-- Create date: < 11 / 1 / 2025 >
//--Description:	< This SP is written to get Medicine details>
//-- =============================================
//--EXEC dbo.GetMedicineDetails @Name = ' ', @CategoryId = 0, @SupplierId = 0;
//ALTER PROCEDURE[dbo].[GetMedicineDetails]
//@Name NVARCHAR(200) = NULL, 
//    @CategoryId INT = NULL,
//    @SupplierId INT = NULL 
//AS
//BEGIN
//    SET NOCOUNT ON;

//SELECT
//        m.MedicineId AS MedicineId,
//        m.Name AS MedicineName,
//        m.CategoryId AS CategoryId,
//        c.Name  AS CategoryName,
//        m.SupplierId AS SupplierId,
//        s.Name AS SupplierName,
//        m.BatchNumber AS BatchNumber,
//        m.StockQuantity AS StockQuantity,
//        m.Price AS Price,
//        CONVERT(date, m.ExpiryDate) AS ExpiryDate,
//        ISNULL(m.ReorderLevel, 0) AS ReorderLevel,
//        ISNULL(m.Deleted, 0)    AS Deleted
//    FROM dbo.Medicines AS m
//    LEFT JOIN dbo.Categories AS c ON m.CategoryId = c.CategoryId
//    LEFT JOIN dbo.Suppliers  AS s ON m.SupplierId = s.SupplierId
//  Where  (
//              LTRIM(RTRIM(ISNULL(@Name, ''))) = '' 
//        OR m.Name LIKE '%' + @Name + '%'
//        )
//        AND (
//            @CategoryId IS NULL 
//            OR @CategoryId = 0 
//            OR m.CategoryId = @CategoryId
//        )
//        AND (
//            @SupplierId IS NULL 
//            OR @SupplierId = 0 
//            OR m.SupplierId = @SupplierId
//        )
//    ORDER BY m.Name;
//END
