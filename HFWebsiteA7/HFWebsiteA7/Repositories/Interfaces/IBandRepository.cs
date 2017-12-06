using HFWebsiteA7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFWebsiteA7.Repositories
{
    interface IBandRepository
    {
        IEnumerable<Band> GetAllBands();
        Band GetBand(int bandId);
        void AddBand(Band band);
    }
}
