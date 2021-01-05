﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
    class Synonyms
    {
        private readonly string ApiToken;
        public Synonyms(string apiToken)
        {
            ApiToken = apiToken;
        }
        public List<string> Query(string vocab)
        {
            List<string> ret = new List<string>();
            try
            {
                WebRequest request = WebRequest.Create(
                  string.Format("http://words.bighugelabs.com/api/2/{0}/{1}/", ApiToken, vocab));
                using WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                using StreamReader reader = new StreamReader(dataStream);
                
                while(!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == "") continue;
                    var parts = line.Split('|');
                    if (parts[1] == "syn") ret.Add(parts[2]);
                }
            }
            catch (Exception) { }
            return ret;
        }
    }
}
