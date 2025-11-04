using AMS.SERVICES.Dapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using PracticeDomain.DTOs.Response;
using PracticeDomain.IRepository;
using PracticeModel.DataModel;
using PracticeModel.DBcontext;

using static PracticeDomain.DTOs.Medicine.Medicine;

namespace PracticeDomain.Repository
{
    public class MedicineRepository : IMedicineRepository
    {

        private readonly BaseContext _dbContext;
        private readonly IDapperService _dapperService;
        public MedicineRepository(BaseContext dbContext, IDapperService dapperService)


        {
            _dbContext = dbContext;
            _dapperService = dapperService;
        }


        public async Task<Response> AddMedicine(MedicineRequest request)
        {
            if (request is not null)
            {
                var medicine = new Medicines
                {
                    Name = request.MedicineName,
                    CategoryId = request.CategoryId,
                    SupplierId = request.SupplierId,
                    BatchNumber = request.BatchNumber,
                    ExpiryDate = request.ExpiryDate,
                    Price = request.Price,
                    StockQuantity = request.StockQuantity,
                };
                _dbContext.Medicines.Add(medicine);
                await _dbContext.SaveChangesAsync();

                return new Response
                {
                    Message = "medicine successfully added",
                    Success = true
                };
            }
            return new Response
            {
                Message = "medicine not added",
                Success = false
            };
        }
        public async Task<Response> UpdateMedicine(MedicineRequest request)
        {
            if (request is not null)
            {

                var medicine = await _dbContext.Medicines.Where(x => x.MedicineId == request.MedicineId).FirstOrDefaultAsync();
                if (medicine is not null)
                {
                    medicine.Name = request.MedicineName;
                    medicine.CategoryId = request.CategoryId;
                    medicine.SupplierId = request.SupplierId;
                    medicine.BatchNumber = request.BatchNumber;
                    medicine.ExpiryDate = request.ExpiryDate;
                    medicine.Price = request.Price;
                    medicine.StockQuantity = request.StockQuantity;
                    await _dbContext.SaveChangesAsync();
                    return new Response
                    {
                        Message = "medicine successfully updated",
                        Success = true
                    };

                }
            }
            return new Response
            {
                Message = "medicine not added",
                Success = false
            };
        }

        public async Task<Response> DeleteMedicine(int Id)
        {
            var medicine = await _dbContext.Medicines.Where(x => x.MedicineId == Id && x.Deleted != true).FirstOrDefaultAsync();
            if (medicine != null)
            {
                medicine.Deleted = true;
                await _dbContext.SaveChangesAsync();
                return new Response
                {
                    Message = "medicine deleted successfully",
                    Success = true
                };
            }
            return new Response
            {
                Message = "medince not deleted",
                Success = false
            };
        }

        public async Task<Response> GetMedicineDetail(MedicineRequestList request)
            {
                DynamicParameters parameters = new();

                parameters.Add("@Name", request.Name );
                parameters.Add ("@CategoryId" ,request.CategoryId);
                parameters.Add("@SupplierId", request.SupplierId);
                ////    parameters.Add("@PageNumber", user.PageNumber);
                //  parameters.Add("@PageSize", user.PageSize);


                var medicineList = await _dapperService.ReturnListAsync<MedicineResponseList>("dbo.GetMedicineDetails", parameters);
                if (medicineList == null || !medicineList.Any())
                {
                return new Response
                {
                    Message = "medince not found",
                    Success = false
                };

            }
            return new Response
            {
                  
                Data = medicineList
            };

            // Deserialize the JSON into the DegreeDetails list
        }
        }
}
