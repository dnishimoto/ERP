using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Millennium.EntityFramework;


namespace Millennium.Services
{
    public class AddressBookRepository: Repository<AddressBook>
    {
        Entities _dbContext;
        public AddressBookRepository(DbContext db):base(db)
        {
            _dbContext = (Entities) db;
        }
        
        public async Task<List<AddressBook>> GetAddressBooks(string keyCode)
        {


                Task<List<AddressBook>> resultList = (from a in _dbContext.AddressBooks
                                                                      join b in _dbContext.UDCs on a.PeopleXrefId equals b.XRefId
                                                                      where b.KeyCode == keyCode
                                                                      orderby a.Name
                                                                      select a

                    ).ToListAsync<AddressBook>();

                    return await resultList;


        }
    }
}
