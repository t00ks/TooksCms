using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TooksCms.Core.Interfaces.Repository;
using TooksCms.ServiceLayer.Bases;

namespace TooksCms.Web.Controllers.API
{
    public class BulletinController : ApiController
    {
        private ILookupRepository _lookupRepository;
        private IBulletinRepository _bulletinRepository;

        public BulletinController(ILookupRepository lookupRepository, IBulletinRepository bulletinRepository)
        {
            _lookupRepository = lookupRepository;
            _bulletinRepository = bulletinRepository;
        }

        public HttpResponseMessage Get(int page = 0)
        {
            try
            {
                int skip = 10 * page;
                var bulletins = BulletinBase.GetList(_lookupRepository, _bulletinRepository, 10, skip).Select(b => b.GetJSONModel());

                if (bulletins == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }

                return Request.CreateResponse(HttpStatusCode.OK, bulletins);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
