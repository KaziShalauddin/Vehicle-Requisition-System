using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRequisitionSystem.Models.EntityModels;
using VehicleRequisitionSystem.Repositories;

namespace VehicleRequisitionSystem.BLL
{
    public class RequestManager
    {
        RequestRepositiory repository = new RequestRepositiory();
        public bool Add(Request request)
        {
            if (string.IsNullOrEmpty(request.Description))
            {
                throw new Exception("Description is not provided!");
            }

            if (string.IsNullOrEmpty(request.Location))
            {
                throw new Exception("Location is not provided!");
            }

            return repository.Add(request);
        }

        public bool Update(Request request)
        {
            return repository.Update(request);
        }

        public bool Remove(Request request)
        {
            return repository.Remove(request);
        }

        public List<Request> GetAll(bool withDeleted = false)
        {
            return repository.GetAll(withDeleted);
        }

        public Request GetById(int id)
        {
            return repository.GetById(id);
        }

        public void Dispose()
        {
            repository.Dispose();
        }
    }
}
