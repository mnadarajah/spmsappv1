using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMSCAV1.Services.Repository
{
    public class DocumentTypeService : BaseBusinessService<DocumentTypeModel>, IDocumentTypeService
    {
        public DocumentTypeService()
        {
            _route = "DocumentTypes";
        }
    }
}
