using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Ffd.Common;

namespace Ffd.Data
{
    /// <summary>
    /// Make it a bit easier to parse an xml document without a lot of redundant extra typing and null checking.
    /// </summary>
    public class XmlDocExtra : XmlDocument
    {
        private XmlNamespaceManager _nsmgr;

        public XmlNamespaceManager NamespaceManager
        {
            get { return _nsmgr; }
            set { _nsmgr = value; }
        }

        private string _currentPath;

        public string CurrentPath
        {
            get { return _currentPath; }
            set { _currentPath = value; }
        }

        public string GetFirstValueFromExtraPath(string extraPath)
        {
            return GetValueFromExtraPath(extraPath, 0);
        }

        public string GetValueFromExtraPath(string extraPath, int index)
        {
            string result = string.Empty;
            string fullPath = string.Format(Functions.BuildStringFromElementsWithDelimiter(_currentPath, extraPath, "/"));
            XmlNodeList nodes = SelectNodes(fullPath, _nsmgr);

            if (nodes.Count > index)
            {
                result = nodes[index].InnerText;
            }
            return result;
        }

        public XmlDocExtra(string xml, string currentPath, XmlNamespaceManager nsmgr)
        {
            LoadXml(xml);
            _currentPath = currentPath;
            _nsmgr = nsmgr;
        }
    }

}
