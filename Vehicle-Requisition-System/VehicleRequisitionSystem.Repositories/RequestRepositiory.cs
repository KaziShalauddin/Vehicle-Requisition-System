using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRequisitionSystem.Models.DBContext;
using VehicleRequisitionSystem.Models.EntityModels;

namespace VehicleRequisitionSystem.Repositories
{
    public class RequestRepositiory
    {
        VehicleRequisitionDBContext db = new VehicleRequisitionDBContext();
        public bool Add(Request request)
        {
            db.Requests.Add(request);
            return db.SaveChanges() > 0;
        }

        public bool Update(Request request)
        {
            db.Requests.Attach(request);
            db.Entry(request).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }

        public bool Remove(Request request)
        {
            request.IsDeleted = true;
            return Update(request);

        }

        public List<Request> GetAll(bool withDeleted = false)
        {
            return db.Requests.Where(c => c.IsDeleted == withDeleted).ToList();
        }

        public Request GetById(int id)
        {
            return db.Requests.FirstOrDefault(c => c.Id == id);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
