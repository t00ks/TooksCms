using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Interfaces.Repository;

namespace TooksCms.DAL
{
    public class SnapshotRepository : ISnapshotRepository
    {
        public IEnumerable<ISnapshot> Fetch(bool includeHtml = false)
        {
            var db = new TooksCmsDAL();

            if (includeHtml)
            {
                return db.Snapshots;
            }

            return db.GetSnapshotLite();
        }

        public ISnapshot Fetch(string url)
        {
            var db = new TooksCmsDAL();
            var trimmedUrl = url.Trim('/');
            if (db.Snapshots.Any(s => s.Url == url))
            {
                return db.Snapshots.FirstOrDefault(s => s.Url == url);
            }
            else if (db.StaticRoutes.Any(r => r.StaticRoute1 == trimmedUrl))
            {
                var sr = db.StaticRoutes.First(r => r.StaticRoute1 == trimmedUrl);
                var staticUrl = "/article/" + (sr.Action == "StaticReview" ? "Review/" : "View/") + sr.Id.ToString();
                return db.Snapshots.FirstOrDefault(s => s.Url == staticUrl);
            }

            return null;
        }

        public void UpdateOrInsert(ISnapshot data)
        {
            var db = new TooksCmsDAL();

            if(db.Snapshots.Any(s => s.Url == data.Url))
            {
                var sh = db.Snapshots.Single(s => s.Url == data.Url);
                sh.Update(data);
            }
            else
            {
                var sh = Snapshot.CreateSnapshot(data);
                db.Snapshots.Add(sh);
            }

            db.SaveChanges();
        }
    }
}
