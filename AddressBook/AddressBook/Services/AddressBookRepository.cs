using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AddressBook.Services
{
    public class AddressBookRepository
    {
        public async Task<List<Millennium.EntityFramework.AddressBook>> GetAddressBooks(string keyCode)
        {
            //List<EntityFramework.AddressBook> resultList = new List<EntityFramework.AddressBook>();
            //UDCRepository udcRepository = new UDCRepository();
            //long xRefId = udcRepository.GetUdcByKeyCode(keyCode);
            using (var db = new Millennium.EntityFramework.Entities())
            {
                /*
                var query = from a in db.AddressBooks
                            join b in db.UDCs on a.PeopleXrefId equals b.XRefId
                            where b.KeyCode == keyCode
                            select a;
                            */

                Task<List<Millennium.EntityFramework.AddressBook>> resultList = (from a in db.AddressBooks
                                                                      join b in db.UDCs on a.PeopleXrefId equals b.XRefId
                                                                      where b.KeyCode == keyCode
                                                                      orderby a.Name
                                                                      select a

                    ).ToListAsync<Millennium.EntityFramework.AddressBook>();

                //query = query.OrderBy(a => a.Name);


                /*foreach (var item in query)
                {
                    resultList.Add(item);
                }
                */

                return await resultList;


            }
        }
    }
}
