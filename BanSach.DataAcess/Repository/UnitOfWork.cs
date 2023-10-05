using BanSach.DataAcess.Data;
using BanSach.DataAcess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanSach.DataAcess.Repository
{
    public class UnitOfWork : IUnitOfWork

    {
        public ICategoryRepository Category { get; private set; }

        public ICoverTypeRepository covertype { get; private set; }

        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            covertype = new CoverTypeRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
    
}
