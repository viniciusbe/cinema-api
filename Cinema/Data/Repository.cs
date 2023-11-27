using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Helpers;
using Cinema.Models;

namespace Cinema.Data
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;

        }
        public void Add(Session session)
        {
            _context.Add(session);
        }

        public Session[] GetAllSessions()
        {
            try
            {

                return _context.Sessions.ToList().ToArray();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public Session? GetSessionById(Guid id)
        {
            return _context.Find<Session>(id);

        }

        public void Update(Session session)
        {
            _context.Update(session);
        }
        public void Delete(Guid id)
        {
            Session? session = GetSessionById(id);
            if (session != null)
            {
                _context.Remove(session);
            }
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }


    }
}