using log4net;
using System.Data.SqlClient;
using Temporary_Prison.Common.Entities;
using Temporary_Prison.Service.Contracts.Dto;
using System;

namespace Temporary_Prison.Service.Contracts.Contracts
{
    public class PrisonerService : IPrisonerService
    {
        private readonly DataAccessService dataService = new DataAccessService();
        private readonly ILog log = LogManager.GetLogger("LOGGER");

        public PrisonerDto GetPrisonerById(int Id)
        {
            if (Id != default(int))
            {
                var prisoner = dataService.ExecProcGetModel<PrisonerDto>("GetPrisonerById", new SqlParameter(@"prisonerId", Id));
                var phones = dataService.ExecProcGetModels<Phone>("GetPhoneNumbers", new SqlParameter(@"prisonerId", Id));

                if (phones != null)
                {
                    prisoner.PhoneNumbers = new string[phones.Length];
                    for (int i = 0; i < phones.Length; i++)
                    {
                        prisoner.PhoneNumbers[i] = phones[i].PhoneNumber;
                    }
                }

                return prisoner;
            }
            return default(PrisonerDto);
        }

        public PrisonerDto[] GetPrisonersForPagedList(int skip, int rowSize, out int totalCount, 
            DateTime? filterByDetainedDate, DateTime? filterByReleasedDate)
        {
            if (rowSize != default(int))
            {
                var param = new SqlParameter[]
                     {
                        new SqlParameter(@"skip",skip),
                        new SqlParameter(@"rowSize",rowSize),
                        new SqlParameter(@"filterByDetainedDate",filterByDetainedDate),
                        new SqlParameter(@"filterByReleasedDate",filterByReleasedDate)
                     };
                return dataService.ExecProcGetModels<PrisonerDto, int>("GetPrisonersToPagedList", "TotalCount", out totalCount, param);
            }
            totalCount = default(int);
            return default(PrisonerDto[]);
        }

        public void AddPrisoner(PrisonerDto prisoner)
        {
            if (prisoner != null)
            {
                dataService.ExecNonQuery("insertPrisoner", prisoner, "newID", out int lasID);
                var countPhones = prisoner.PhoneNumbers.Length;
                var phones = new Phone[countPhones];
                if (lasID != default(int))
                {
                    for (int i = 0; i < countPhones; i++)
                    {
                        phones[i] = new Phone()
                        {
                            PrisonerId = lasID,
                            PhoneNumber = prisoner.PhoneNumbers[i]
                        };
                    }
                    dataService.ExecNonQuery("dbo.InsertPhoneNumbers", phones);
                 
                }
            }
        }

        public void DeletePrisoner(int id)
        {
            if (id != default(int))
            {
                dataService.ExecNonQuery("DeletePrisoner", new SqlParameter("@Priosner_ID", id));
            }
        }

        public void EditPrisoner(PrisonerDto prisoner)
        {
            if (prisoner != null)
            {
                dataService.ExecNonQuery("EditPrisoner", prisoner);
                var countPhones = prisoner.PhoneNumbers.Length;
                var phones = new Phone[countPhones];
                for (int i = 0; i < countPhones; i++)
                {
                    phones[i] = new Phone()
                    {
                        PrisonerId = prisoner.PrisonerId,
                        PhoneNumber = prisoner.PhoneNumbers[i]
                    };
                }
                dataService.ExecNonQuery("dbo.InsertPhoneNumbers", phones);
            }
        }
        public DetentionPagedListDto[] GetDetentionsByPrisonerIdForPagedList(int Id, int skip, int rowSize, out int totalCount)
        {
            if (rowSize > 0)
            {
                var parametrs = new SqlParameter[]
                {
                new SqlParameter("@PrisonerID",Id),
                new SqlParameter("@skip",skip),
                new SqlParameter("@rowSize",rowSize)
                };
                return dataService.ExecProcGetModels<DetentionPagedListDto, int>("GetDetentionsByIdForPagedList", "totalCount", out totalCount, parametrs);
            }
            totalCount = default(int);
            return default(DetentionPagedListDto[]);
        }

        public void RegisterDetention(RegistrationOfDetentionDto registrationOfDetention)
        {
            if (registrationOfDetention != null)
            {
                dataService.ExecNonQuery("insertEmployee",
                    registrationOfDetention.DetainedEmployee,
                    "employeeID", out int _DetainedEmployeeID);

                dataService.ExecNonQuery("insertEmployee",
                    registrationOfDetention.DeliveredEmployee,
                    "employeeID", out int _DeliveredEmployeeID);

                dataService.ExecNonQuery("InsertDeliveryProcedures",
                    "DeliveryProceduresID", out int _DeliveryProceduresID,
                    new SqlParameter("@employeeID", _DeliveredEmployeeID));

                dataService.ExecNonQuery("InsertDetentionProcedures",
                    "DetentionProceduresID", out int _DetentionProceduresID,
                    new SqlParameter("@employeeID", _DetainedEmployeeID));

                var regist = new RegistrationOfDetention()
                {
                    PrisonerID = registrationOfDetention.PrisonerID,
                    DateOfDetention = registrationOfDetention.DateOfDetention,
                    DateOfArrival = registrationOfDetention.DateOfArrival,
                    DeliveredProceduresID = _DeliveryProceduresID,
                    DetentionProceduresID = _DetentionProceduresID,
                    PlaceofDetention = registrationOfDetention.PlaceofDetention
                };
                dataService.ExecNonQuery("RegistrationOfDetention", regist);
            }
        }

        public DetentionDto GetDetentionById(int id)
        {
            if (id != default(int))
            {
                var detention = dataService.ExecProcGetModel<Detention>("GetDetentionById", new SqlParameter("@DetentionId", id));

                if (detention != null)
                {
                    var ReleaseEmpl = dataService.ExecProcGetModel<EmployeeDto>("GetEmployeeByExecutionProcedureID",
                        new SqlParameter("@ExecProcedureID", detention.ReleaseProceduresID),
                        new SqlParameter("@ExecuProcName", "Release"));

                    var DetainedEmpl = dataService.ExecProcGetModel<EmployeeDto>("GetEmployeeByExecutionProcedureID",
                        new SqlParameter("@ExecProcedureID", detention.DetentionProceduresID),
                        new SqlParameter("@ExecuProcName", "Detention"));

                    var DeliveredEmpl = dataService.ExecProcGetModel<EmployeeDto>("GetEmployeeByExecutionProcedureID",
                        new SqlParameter("@ExecProcedureID", detention.DeliveredProceduresID),
                        new SqlParameter("@ExecuProcName", "Delivery"));

                    var detentionDto = new DetentionDto()
                    {
                        DetentionID = detention.DetentionID,
                        PrisonerID = detention.PrisonerID,
                        PlaceofDetention = detention.PlaceofDetention,
                        DateOfDetention = detention.DateOfDetention,
                        DateOfArrival = detention.DateOfArrival,
                        DateOfRelease = detention.DateOfRelease,
                        AccruedAmount = detention.AccruedAmount,
                        DeliveredEmployee = DeliveredEmpl,
                        DetainedEmployee = DetainedEmpl,
                        ReleasedEmployee = ReleaseEmpl,
                        PaidAmount = detention.PaidAmount
                    };

                    return detentionDto;
                }
            }
            return default(DetentionDto);
        }

        public void ReleaseOfPrisoner(ReleaseOfPrisonerDto release)
        {
            if (release != null)
            {
                dataService.ExecNonQuery("insertEmployee",
                         release.ReleasedEmployee,
                        "employeeID", out int _ReleasedEmployeeID);

                dataService.ExecNonQuery("InsertReleaseProcedures",
                      "ReleaseProceduresID",
                      out int _ReleasedProceduresID,
                      new SqlParameter("@employeeID", _ReleasedEmployeeID));

                var ReleaseOfPrisoner = new ReleaseOfPrisoner()
                {
                    AccruedAmount = release.AccruedAmount,
                    DateOfRelease = release.DateOfRelease,
                    DetentionID = release.DetentionID,
                    PaidAmount = release.PaidAmount,
                    ReleaseProceduresID = _ReleasedProceduresID
                };

                dataService.ExecNonQuery("ReleaseOfPrisoner", ReleaseOfPrisoner);
            }
        }

        public void EditDetention(DetentionDto detention)
        {
            if (detention != null)
            {
                var detentionEntity = new Detention()
                {
                    DetentionID = detention.DetentionID,
                    AccruedAmount = detention.AccruedAmount ?? default(decimal),
                    PaidAmount = detention.PaidAmount ?? default(decimal),
                    DateOfArrival = detention.DateOfArrival,
                    DateOfDetention = detention.DateOfDetention,
                    DateOfRelease = detention.DateOfRelease,
                    PlaceofDetention = detention.PlaceofDetention
                };
                dataService.ExecNonQuery("EditDetention", detentionEntity);

                dataService.ExecNonQuery("EditEmployee", detention.DeliveredEmployee);
                dataService.ExecNonQuery("EditEmployee", detention.DetainedEmployee);
                if (detention.ReleasedEmployee != null)
                {
                    dataService.ExecNonQuery("EditEmployee", detention.ReleasedEmployee);
                }
            }
        }

        public void DeleteDetention(int id)
        {
            dataService.ExecNonQuery("DeleteDetention", new SqlParameter("@id", id));
        }

        public PrisonerDto[] SearchFilter(DateTime? dateOfDetention, string name, string address)
        {
            var parametrs = new SqlParameter[]
            {
                new SqlParameter("@DateOfDetention", dateOfDetention ?? (object)DBNull.Value),
                new SqlParameter("@Name", name ?? (object)DBNull.Value),
                new SqlParameter("@Address", address ?? (object)DBNull.Value)
            };

            return dataService.ExecProcGetModels<PrisonerDto>("SearchFilter", parametrs);
        }
    }
}
