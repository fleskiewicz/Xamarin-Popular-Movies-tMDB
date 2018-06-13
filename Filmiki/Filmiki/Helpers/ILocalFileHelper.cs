using System;
using System.Collections.Generic;
using System.Text;

namespace Filmiki.Helpers
{
    public interface ILocalFileHelper
    {
        string GetLocalFilePath(string fileName);
    }
}
