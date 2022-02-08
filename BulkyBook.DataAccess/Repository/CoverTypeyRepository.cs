using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAccess;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class CoverTypeyRepository : Repository<CoverType>, ICoverTypeRepository

    {
        private readonly ApplicationDbContext _db;
        public CoverTypeyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
            
        }
        public void Update(CoverType obj)
        {
           _db.CoverTypes.Update(obj);
        }
    }
}
