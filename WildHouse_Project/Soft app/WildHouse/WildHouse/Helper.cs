using System;

namespace WildHouse
{
    public class Helper
    {

       public Helper() { }

       public bool questVerify(int user, int pet)
       {
            bool k = true;
            while(user>0)
            {
                if ((pet % 10) > (user % 10))
                {
                    k = false;
                    return k;
                }
                user = user / 10;
                pet = pet / 10;
            }
            return k;
       }

        public string[] clearString(string[] s)
        {
            for(int i=0; i<s.Length;i++)
            {
                s[i] = null;
            }
            return s;
        }

        public bool checkTextToInt(string s)
        {
            bool k = true;
            try
            {
                int c = Convert.ToInt32(s);
            }
            catch
            {
                k = false;
            }
            return k; 
        }
    }
}
