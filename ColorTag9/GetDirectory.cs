using System;
using System.IO;

namespace ColorTag
{
    public class GetDirectory
    {
        public string GetParentDirectory(int levels)
        {
            string parentPath = Path.GetDirectoryName(Plugin.plugin.ConfigPath);
            for (int i = 0; i < levels; i++)
            {
                parentPath = Directory.GetParent(parentPath)?.FullName;
                if (parentPath == null)
                {
                    throw new InvalidOperationException("It is impossible to go higher than the root directory.");
                }
            }
            return parentPath;
        }
    }
}
