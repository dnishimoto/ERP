using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.BuyerDomain
{
public class FluentBuyerQuery:MapperAbstract<Buyer, BuyerView>,IFluentBuyerQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentBuyerQuery() { }
        public FluentBuyerQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<Buyer> MapToEntity(BuyerView inputObject)
        {
           Buyer outObject = mapper.Map<Buyer>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<List<Buyer>> MapToEntity(List<BuyerView> inputObjects)
        {
            List<Buyer> list = new List<Buyer>();
            foreach (var item in inputObjects)
            {
                Buyer outObject = mapper.Map<Buyer>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public override  async Task<BuyerView> MapToView(Buyer inputObject)
        {
            BuyerView outObject = mapper.Map<BuyerView>(inputObject);

            AddressBook address = await _unitOfWork.addressBookRepository.GetEntityById(inputObject.AddressId);

            outObject.BuyerName = address.Name;

            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.buyerRepository.GetNextNumber(TypeOfBuyer.BuyerNumber.ToString());
        }
 public override async Task<BuyerView> GetViewById(long ? buyerId)
        {
            Buyer detailItem = await _unitOfWork.buyerRepository.GetEntityById(buyerId);

            return await MapToView(detailItem);
        }
 public async Task<BuyerView> GetViewByNumber(long buyerNumber)
        {
            Buyer detailItem = await _unitOfWork.buyerRepository.GetEntityByNumber(buyerNumber);

            return await MapToView(detailItem);
        }

public override async Task<Buyer> GetEntityById(long ? buyerId)
        {
            return await _unitOfWork.buyerRepository.GetEntityById(buyerId);

        }
 public async Task<Buyer> GetEntityByNumber(long buyerNumber)
        {
            return await _unitOfWork.buyerRepository.GetEntityByNumber(buyerNumber);
        }
}
}
