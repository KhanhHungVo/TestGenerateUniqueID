using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestGenerateID
{
    class Program
    {
        private static Random random = new Random();
        static void Main(string[] args)
        {
            int size = 1402410240;
            int i = 0;
            while (i < size){
                string _key1 = Program.RandomString(6);
                string _key2 = Program.RandomString2(6);
                using (var entity = new TestEntities())
                {
                    entity.GenerateTests.Add(new GenerateTest
                    {
                        key1 = _key1,
                        key2 = _key2,
                        CreatedAt = DateTime.Now

                    });
                    entity.SaveChanges();
                }
            }
            
        }
        static string RandomString(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyz1234567890";
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }

            return res.ToString();
        }


        public static string RandomString2(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
   



}
