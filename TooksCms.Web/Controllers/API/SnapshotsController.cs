using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TooksCms.Core.Interfaces.Repository;
using TooksCms.ServiceLayer.Objects;

namespace TooksCms.Web.Controllers.API
{
    public class SnapshotsController : ApiController
    {
        private ISnapshotRepository _repository;

        public SnapshotsController(ISnapshotRepository repository)
        {
            _repository = repository;
        }

        public HttpResponseMessage Get()
        {
            try
            {
                var snapshots = _repository.Fetch().Select(s => new Snapshot(s)).ToList();

                if (snapshots.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, snapshots.ToDictionary(k => k.Url, v => v.Date));
                }

                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(Snapshot snapshot)
        {
            try
            {
                _repository.UpdateOrInsert(snapshot);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
