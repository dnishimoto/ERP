using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.SupervisorDomain
{
public class FluentSupervisorQuery:MapperAbstract<Supervisor,SupervisorView>, IFluentSupervisorQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentSupervisorQuery() { }
        public FluentSupervisorQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<Supervisor> MapToEntity(SupervisorView inputObject)
        {

            Supervisor outObject = mapper.Map<Supervisor>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<IList<Supervisor>> MapToEntity(IList<SupervisorView> inputObjects)
        {
            IList<Supervisor> list = new List<Supervisor>();

            foreach (var item in inputObjects)
            {
                Supervisor outObject = mapper.Map<Supervisor>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public override async Task<SupervisorView> MapToView(Supervisor inputObject)
        {

            SupervisorView outObject = mapper.Map<SupervisorView>(inputObject);

            if (inputObject.ParentSupervisorId!=null)
            {
                Task<Supervisor> parentTask = _unitOfWork.supervisorRepository.GetEntityById(inputObject?.ParentSupervisorId);
                Task<AddressBook> addressBookTask = _unitOfWork.addressBookRepository.GetEntityById(inputObject.AddressId);
                Task<Udc> udcTitleTask =  _unitOfWork.udcRepository.GetEntityById(inputObject.JobTitleXrefId);

                Task.WaitAll(parentTask, addressBookTask, udcTitleTask);

                outObject.SupervisorName = addressBookTask.Result.Name;
                outObject.JobTitle = udcTitleTask.Result.Value;
              

                if (parentTask.Result != null)
                {
                    Task<Udc> titleTask = _unitOfWork.udcRepository.GetEntityById(parentTask.Result?.JobTitleXrefId);
                    Task<AddressBook> parentAddressBookTask = _unitOfWork.addressBookRepository.GetEntityById(parentTask.Result.AddressId);
                    Task.WaitAll(titleTask, addressBookTask);

                    outObject.ParentSupervisorName = addressBookTask.Result.Name;
                    outObject.ParentSupervisorId = inputObject.ParentSupervisorId;
                    outObject.ParentSupervisorAddressId = parentTask.Result.AddressId;
                    outObject.ParentSupervisorTitle = titleTask.Result.Value;
                    outObject.ParentSupervisorCode = parentTask.Result.SupervisorCode;
                }

            }
            await Task.Yield();
            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfSupervisor.SupervisorNumber.ToString());
        }
 public override async Task<SupervisorView> GetViewById(long ? supervisorId)
        {
            Supervisor detailItem = await _unitOfWork.supervisorRepository.GetEntityById(supervisorId);

            return await MapToView(detailItem);
        }
 public async Task<SupervisorView> GetViewByNumber(long supervisorNumber)
        {
            Supervisor detailItem = await _unitOfWork.supervisorRepository.GetEntityByNumber(supervisorNumber);

            return await MapToView(detailItem);
        }

public override async Task<Supervisor> GetEntityById(long ? supervisorId)
        {
            return await _unitOfWork.supervisorRepository.GetEntityById(supervisorId);

        }
 public async Task<Supervisor> GetEntityByNumber(long supervisorNumber)
        {
            return await _unitOfWork.supervisorRepository.GetEntityByNumber(supervisorNumber);
        }
}
}
