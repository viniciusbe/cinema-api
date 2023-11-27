using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Models;

namespace Cinema.Data
{
    public interface IRepository
    {
        void Add(Session session);
        Session? GetSessionById(Guid id);
        Session[] GetAllSessions();
        void Update(Session session);
        void Delete(Guid id);
        public bool SaveChanges();

    }
}