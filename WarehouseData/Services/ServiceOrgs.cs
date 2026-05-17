using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseData.Context;
using WarehouseData.Models;

namespace WarehouseData.Services
{
    public interface IServiceOrgs
    {
        public ApplicationContext GetContext();

        public bool AddOrg(Organization org);

        public bool EditOrg(Organization org);

        public bool DelOrg(Organization org);
    }

    public class ServiceOrgs : IServiceOrgs
    {
        private ApplicationContext _context {  get; set; }

        public ServiceOrgs(ApplicationContext context) 
        { 
            this._context = context; 
        }

        public bool AddOrg(Organization org)
        {
            bool result = false;
            if (org != null)
            { 
                if(!String.IsNullOrEmpty(org.OrgName))
                {
                    if (!this._context.orgs.Contains(org))
                    {
                        this._context.orgs.Add(org);
                        result = true;
                    }
                }
            }
            return result;
        }

        public bool EditOrg(Organization org) 
        {
            bool result = false;
            if (org != null)
            {
                if (!String.IsNullOrEmpty(org.OrgName))
                {
                   Organization? target = _context.orgs.
                        FirstOrDefault(o => o.OrgId == org.OrgId);
                    if (target != null) 
                    { 
                        int id = _context.orgs.IndexOf(target);
                        if (id != -1) _context.orgs[id] = org;
                    }
                    result = true;
                }
            }
            return result;
        }

        public bool DelOrg(Organization org)
        {
            bool result = false;
            if (org != null)
            {
                if (!String.IsNullOrEmpty(org.OrgName))
                {
                    if (this._context.orgs.Contains(org))
                    {
                        this._context.orgs.Remove(org);
                        result = true;
                    }
                }
            }
            return result;
        }

        public ApplicationContext GetContext()
        {
            return _context;
        }
    }
}
