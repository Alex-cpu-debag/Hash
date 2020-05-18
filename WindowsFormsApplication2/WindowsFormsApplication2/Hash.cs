using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace hashing
{
    public enum Supported_HA
    {
        SHA256, SHA384, SHA512, MD5
    }

    class Hashing
    {
        public static string ComputeHash(string plainText, Supported_HA hash, byte[] salt)
        {
            int minSaltLength = 4, maxSaltLength = 16;

            byte[] SaltBytes = null;
            if (salt != null)
            {
                SaltBytes = salt;
            }
            else
            {
                Random r = new Random();
                int SaltLength = r.Next(minSaltLength, maxSaltLength);
                SaltBytes = new byte[SaltLength];
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                rng.GetNonZeroBytes(SaltBytes);
                rng.Dispose();
            }


            byte[] plainData = ASCIIEncoding.UTF8.GetBytes(plainText);
            byte[] plainDataWithSalt = new byte[plainData.Length + SaltBytes.Length];

            for (int x = 0; x < plainData.Length; x++)
                plainDataWithSalt[x] = plainData[x];
            for (int n = 0; n < SaltBytes.Length; n++)
                plainDataWithSalt[plainData.Length + n] = SaltBytes[n];

            byte[] hashValue = null;

            switch (hash)
            {
                case Supported_HA.SHA256:
                    SHA256Managed sha = new SHA256Managed();
                    hashValue = sha.ComputeHash(plainDataWithSalt);
                    sha.Dispose();
                    break;
                case Supported_HA.SHA384:
                    SHA384Managed sha1 = new SHA384Managed();
                    hashValue = sha1.ComputeHash(plainDataWithSalt);
                    sha1.Dispose();
                    break;
                case Supported_HA.SHA512:
                    SHA512Managed sha2 = new SHA512Managed();
                    hashValue = sha2.ComputeHash(plainDataWithSalt);
                    sha2.Dispose();
                    break;
            }

            byte[] result = new byte[hashValue.Length + SaltBytes.Length];
            for (int x = 0; x < hashValue.Length; x++)
                result[x] = hashValue[x];
            for (int n = 0; n < SaltBytes.Length; n++)
                result[hashValue.Length + n] = SaltBytes[n];

            return Convert.ToBase64String(result);
        }
        /// <summary>
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="hash"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string ComputeHashNoSalt(string plainText, Supported_HA hash)
        {
            byte[] plainData = ASCIIEncoding.UTF8.GetBytes(plainText);

            for (int x = 0; x < plainData.Length; x++) ;


            byte[] hashValue = null;

            switch (hash)
            {
                case Supported_HA.SHA256:
                    SHA256Managed sha = new SHA256Managed();
                    hashValue = sha.ComputeHash(plainData);
                    sha.Dispose();
                    break;
                case Supported_HA.SHA384:
                    SHA384Managed sha1 = new SHA384Managed();
                    hashValue = sha1.ComputeHash(plainData);
                    sha1.Dispose();
                    break;
                case Supported_HA.SHA512:
                    SHA512Managed sha2 = new SHA512Managed();
                    hashValue = sha2.ComputeHash(plainData);
                    sha2.Dispose();
                    break;
            }

            byte[] result = new byte[hashValue.Length];
            for (int x = 0; x < hashValue.Length; x++)
                result[x] = hashValue[x];

            return Convert.ToBase64String(result);
        }
        /// <summary>
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="hashValue"></param>
        /// <param name="hash"></param>
        /// <returns></returns>

        public static bool Confirm(string plainText, string hashValue, Supported_HA hash)
        {
            byte[] hashBytes = Convert.FromBase64String(hashValue);

            int hashSize = 0;

            switch (hash)
            {
                case Supported_HA.SHA256:
                    hashSize = 32;
                    break;
                case Supported_HA.SHA384:
                    hashSize = 48;
                    break;
                case Supported_HA.SHA512:
                    hashSize = 64;
                    break;
            }

            byte[] SaltBytes = new byte[hashBytes.Length - hashSize];

            for (int x = 0; x < SaltBytes.Length; x++)
                SaltBytes[x] = hashBytes[hashSize + x];

            string newHash = ComputeHash(plainText, hash, SaltBytes);

            return (hashValue == newHash);
        }
      /*  public static string ComputeHashMD5(string plainText, Supported_HA hash)
        {	
          // создаем объект этого класса. Отмечу, что он создается не через new, а вызовом метода Create
          MD5 md5Hasher = MD5.Create();
          // Преобразуем входную строку в массив байт и вычисляем хэш
          byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
          // Создаем новый Stringbuilder (Изменяемую строку) для набора байт
          StringBuilder sBuilder = new StringBuilder();
          // Преобразуем каждый байт хэша в шестнадцатеричную строку
          for (int i = 0; i < data.Length; i++)
          {    
              //указывает, что нужно преобразовать элемент в шестнадцатиричную строку длиной в два символа    
              sBuilder.Append(data[i].ToString("x2"));
          }          
          sBuilder.ToString();        
          string pasHash = getMd5Hash("Hash");
        }
        */

        public static string CalculateMD5Hash(string input, Supported_HA hash)
        {

            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashh = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashh.Length; i++)
            {
                sb.Append(hashh[i].ToString("X2"));
            }
            return sb.ToString();
        }
        
    }
    /// <summary>
    ///
    /// </summary>
  /*  public class HMACSHA512example
    {

        public static void file(string[] Fileargs)
        {
            string dataFile;
            string signedFile;
            //Если имена файлов не указаны, создать их.
            if (Fileargs.Length < 2)
            {
                dataFile = @"text.txt";
                signedFile = "signedFile.enc";

                if (!File.Exists(dataFile))
                {
                    // Создайте файл для записи.
                    using (StreamWriter sw = File.CreateText(dataFile))
                    {
                        sw.WriteLine("Вот сообщение, чтобы подписать");
                    }
                }
            }
            else
            {
                dataFile = Fileargs[0];
                signedFile = Fileargs[1];
            }
            try
            {
                //Создайте случайный ключ с помощью генератора случайных чисел. Это было бы
                //секретный ключ, совместно с отправителем и получателем.
                byte[] secretkey = new Byte[64];
                //RNGCryptoServiceProvider является реализацией генератора случайных чисел.
                using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                {
                    // Массив теперь заполнен криптографически сильными случайными байтами.
                    rng.GetBytes(secretkey);

                    // Используйте секретный ключ, чтобы подписать файл сообщения.
                    SignFile(secretkey, dataFile, signedFile);

                    // Проверить подписанный файл
                    VerifyFile(secretkey, signedFile);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Ошибка: Файл не найден", e);
            }
        }  //end main
        // Вычисляет хэш с ключом для исходного файла и создает целевой файл с помощью хэша с ключом
        // к содержимому исходного файла. 
        public static void SignFile(byte[] key, String sourceFile, String destFile)
        {
            // Инициализовать объект хэша с ключом.
            using (HMACSHA512 hmac = new HMACSHA512(key))
            {
                using (FileStream inStream = new FileStream(sourceFile, FileMode.Open))
                {
                    using (FileStream outStream = new FileStream(destFile, FileMode.Create))
                    {
                        // Вычислите хэш вхотового файла.
                        byte[] hashValue = hmac.ComputeHash(inStream);
                        // Сбросить inStream к началу файла.
                        inStream.Position = 0;
                        // Напишите вычисленный хэш-значение в выводной файл.
                        outStream.Write(hashValue, 0, hashValue.Length);
                        // Копируйте содержимое sourceFile к destFile.
                        int bytesRead;
                        // читать 1K за один раз
                        byte[] buffer = new byte[1024];
                        do
                        {
                            // Читайте с обертывания CryptoStream.
                            bytesRead = inStream.Read(buffer, 0, 1024);
                            outStream.Write(buffer, 0, bytesRead);
                        } while (bytesRead > 0);
                    }
                }
            }
            return;
        } // end SignFile

        // Сравнивает ключ в исходном файле с новым ключом, созданным для части файла данных. Если ключи
        // сравнить данные не были подделаны.
        public static bool VerifyFile(byte[] key, String sourceFile)
        {
            bool err = false;
            // Инициализовать объект хэша с ключом.
            using (HMACSHA512 hmac = new HMACSHA512(key))
            {
                // Создайте массив для хранения считываемого значения хэша с ключами из файла.
                byte[] storedHash = new byte[hmac.HashSize / 8];
                // Создайте ФайлСтрим для исходного файла.
                using (FileStream inStream = new FileStream(sourceFile, FileMode.Open))
                {
                    // Читает данные в storedHash.
                    inStream.Read(storedHash, 0, storedHash.Length);
                    // Вычислите хэш оставшегося содержимого файла.
                    // Поток правильно расположен в начале содержимого,
                    // сразу после сохраненного значения хэша.
                    byte[] computedHash = hmac.ComputeHash(inStream);
                    // сравнить вычисленный хэш с сохраненным значением

                    for (int i = 0; i < storedHash.Length; i++)
                    {
                        if (computedHash[i] != storedHash[i])
                        {
                            err = true;
                        }
                    }
                }
            }
            if (err)
            {
                Console.WriteLine("Хэш значения отличаются! Подписанный файл был подделан!");
                return false;
            }
            else
            {
                Console.WriteLine("Хэш значения согласованы - фальсификации не произошло.");
                return true;

            } //end VerifyFile
        } //end class
        
    }*/
 
}
