using Cocoon.ORM;
using Strive.Common;
using System;
using System.Collections.Generic;

namespace Strive.BusinessEntities.Model
{
    public class DocumentDto
    {
        public List<Document> Document { get; set; }

        public List<GlobalUpload.DocumentType> DocumentType { get; set; }
    }
}