using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GreaterHeights.Domain;
using GreaterHeights.Interfaces;

namespace GreaterHeights.Repositories
{
    using System.Transactions;

    public class AccidentRepository : IAccidentRepository
    {
        private IMonkeyContext context;

        public AccidentRepository(IMonkeyContext context)
        {
            this.context = context;
        }

        public bool Authorised
        {
            //TODO do something here with a claimsprincipal to decide on auth status
            get { return true; }
        }

        public AccidentReport ReportAccident(AccidentReport report)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                this.context.Reports.Add(report);

                this.context.Save();

                report.Id = 1;

                scope.Complete();

                return report;
            }
        }

        public IQueryable<AccidentReport> GetAllAccidents()
        {
            return context.Reports;
        }
    }
}
