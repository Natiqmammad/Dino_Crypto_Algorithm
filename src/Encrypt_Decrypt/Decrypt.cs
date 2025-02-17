
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using System.Windows.Markup;

using System.Diagnostics;


public class Decrypte{
    
    public static Dictionary<string,char> decryptDictionary = new Dictionary<string, char>();
   public static Dictionary<string,char> decryptDictionary_Global = new Dictionary<string, char>();
   
   /*static async Task<string> Key_DecryptAsync(byte[] bytes)
{
    using (Aes aes = Aes.Create())
    {
        aes.IV = Encrypte.iv;
        aes.Key = Encrypte.key;
        aes.KeySize = 128;
        aes.BlockSize = 128;  // AES standard block size
        aes.Padding = PaddingMode.PKCS7;  // Ensure padding mode matches encryption padding


        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using (MemoryStream msDecrypt = new MemoryStream(bytes))
        {
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            {
                using (StreamReader reader = new StreamReader(csDecrypt))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }
    }
}

*/

public static async Task Main_Decrypt(){
    
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write("Pls enter the encryption key :");
    Console.ForegroundColor = ConsoleColor.Magenta;

      
             
        string  encryptionKeyInput = Console.ReadLine().Trim();
    
    if(string.IsNullOrWhiteSpace(encryptionKeyInput)){
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("This Key is Null!");
       Console.ResetColor();
        return;
    }
   
     //byte[] encryptionKeyBytes = Convert.FromBase64String(encryptionKeyInput);

//string decryptedKey =  Key_DecryptAsync(encryptionKeyBytes).GetAwaiter().GetResult();

//Console.WriteLine(decryptedKey);
   string decryptedKey =  AESCipher.Decrypt(encryptionKeyInput);
    
    string[] keyParts = decryptedKey.Split(',');


    Encrypte.default_key = Encrypte.default_key_string.Split(',').Select(s => int.Parse(s.Trim())).ToList();
    
    Echarcters.Choose_Echarcters("Linux");

 DecrypteReverseDictionary(Encrypte.default_key,decryptDictionary);
    

    foreach(var part in keyParts){
       Encrypte.global_key.Add(int.Parse(DecryptedString(part,1)));
    }
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Decrypte key after :" +string.Join(',' , Encrypte.global_key));
    Encrypte.global_key = Key_Decrypt.DecrypteList(Encrypte.global_key);
    Console.WriteLine("Decrypte key before :" +string.Join(',' , Encrypte.global_key));
    
    Console.ForegroundColor = ConsoleColor.Cyan;
    //Console.Write("Encryption Key: ");
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine(string.Join(",", Encrypte.global_key));

    
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write("Text [y/n] :");
    Console.ForegroundColor = ConsoleColor.Magenta;
    string inputv = Console.ReadLine().Trim();
    if(inputv == "y"){
        Console.ForegroundColor = ConsoleColor.Cyan;
         Console.Write("Please enter text:");
         Console.ForegroundColor = ConsoleColor.Magenta;
         try{
    string input_text = Console.ReadLine()
    
    ;
         Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Text :");
        Console.ForegroundColor = ConsoleColor.Green;
          Console.WriteLine(DecryptedString(input_text));}catch(Exception e){
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error:{e.Message}");
            Console.ResetColor();
        }
    }else{
    Console.ForegroundColor = ConsoleColor.Cyan;
   Console.Write("Pls enter encrpyted Directory or File : ");
   Console.ForegroundColor = ConsoleColor.Magenta;
    string inputPath = Console.ReadLine().Trim();
    if(string.IsNullOrWhiteSpace(inputPath)){
        Console.ForegroundColor = ConsoleColor.Red;
        throw new Exception("Error : Path is empty!");
        Console.ResetColor();
        
    }

     

    if(Directory.Exists(inputPath)){
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Your chosed Directory ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(inputPath);
        Console.ForegroundColor = ConsoleColor.Cyan;
       try{
       await  DecrypteFilesAndDirectoriesAsync(inputPath);
      
        }catch (Exception e){
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error:{e.Message}");
            Console.ResetColor();
        }
    }else if(File.Exists(inputPath)){
        try{
       await  FileDecryptAsync(inputPath);
     
      await  FileNameDecryptAsync(inputPath);
     
        }catch (Exception e){
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error:{e.Message}");
            Console.ResetColor();
        }
    }else{
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Error : File doesnt exist!");
        Console.ResetColor();
        throw new Exception("Error:File Or Directory Doesn't Exist!");
    }
    }



}

public static void DecrypteReverseDictionary(List<int> a,Dictionary<string,char> b){
    Key_Decrypt.Decrypt_Key_Dictionary(a);
     foreach(var part in Key_Decrypt.decDictionary){
        b[part.Value] = part.Key;
    }
}

public static async Task DecrypteFilesAndDirectoriesAsync(string directoryPath){
    foreach (var item in Directory.GetFiles(directoryPath))
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"{directoryPath} inside file : {item}");
        await FileDecryptAsync(item);
        await FileNameDecryptAsync(item);
    }

    foreach(var item in Directory.GetDirectories(directoryPath)){
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"{directoryPath} subdirectory: {item}");

       
        await DecrypteFilesAndDirectoriesAsync(item);
    }

    string directoryName = Path.GetFileName(directoryPath);
        string decryptedDirectoryName = DecryptedString(directoryName);
        string parentDirectory = Path.GetDirectoryName(directoryPath);
        string newDirectoryPath = Path.Combine(parentDirectory, decryptedDirectoryName);

        if (!Directory.Exists(newDirectoryPath))
        {
            Directory.Move(directoryPath, newDirectoryPath);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Directory  Decrypted: ");
             Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{directoryPath}  -> {newDirectoryPath}");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Directory name collision:"); 
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{newDirectoryPath} already exists. Skipping.");
        }

}

public static async Task FileDecryptAsync(string directoryPath)
    {
        
            byte[] fileByte = File.ReadAllBytes(directoryPath);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("File Context :");
            Console.ForegroundColor = ConsoleColor.Blue;
           Console.WriteLine($"{Encoding.UTF8.GetString(fileByte)}");
            string decryptedFileContext = DecryptedString(Encoding.UTF8.GetString(fileByte).Trim());
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"Decrypted  Context :");
            Console.ForegroundColor = ConsoleColor.Blue;
             Console.WriteLine($"{decryptedFileContext}");
            File.WriteAllText(directoryPath, decryptedFileContext);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"File Context Decrypted:");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{directoryPath}");

    }

public static string DecryptedString(string cipherText, int number = 0)
{
    StringBuilder builder = new StringBuilder();
    long temp_charter_check =0;
    
    foreach(var i in cipherText){
        if(!Encrypte.characters.Contains(i)){
            temp_charter_check++;
        }
        
    }
    
    long numb = cipherText.Length / 5 ;
    if(temp_charter_check > 0){
      numb =numb - temp_charter_check /5 + temp_charter_check;
    
    }
    
    int test_n = 0;
    
    if (number == 0)
    {
        for (var i = 0; i < numb; i++)
        {

            StringBuilder builder2 = new StringBuilder();
            DecrypteReverseDictionary(Encrypte.global_key,decryptDictionary_Global);
            
           
            
            

                

                
            bool temp_check_found = false;
            for (int j = 0; j < 5; j++)
            { 
                if(!Encrypte.characters.Contains(cipherText[j + test_n])){
                    builder.Append(cipherText[j + test_n]);
                    
                    temp_check_found=true;
                    break;

                }
                builder2.Append(cipherText[j + test_n]);
                
            }
                
            
            

             
            if(temp_check_found){
                builder2.Clear();
                    test_n+=1;
            }
            else if(builder2.ToString() ==Encrypte.encrpyte_newline){
                     builder.Append('\n');
                    
                    
                }else if(builder2.ToString() ==Encrypte.encrypte_dirnaq){
                     builder.Append('"');
                    
                    
                }
                
                else{
            foreach (var item in decryptDictionary_Global)
            {
                if (item.Key == builder2.ToString())
                {
                    builder.Append(item.Value);
                    
                    break;
                }

                
            }
                }

            /*if (!found)
            {
                //Console.ForegroundColor = ConsoleColor.Red;
                //throw new Exception($"Error:This symbol doenst decrypted! : {builder2.ToString()}");
                
            }*/
            
            if(!temp_check_found){
            test_n += 5;  
            builder2.Clear();
            }

        }
    }
    else
    {
        
        
        
        for (var i = 0; i < numb; i++)
        {
            StringBuilder builder2 = new StringBuilder();
            for (int j = 0; j < 5; j++)
            {
                builder2.Append(cipherText[j + test_n]);
            }

             bool found = false;
            foreach (var item in decryptDictionary)
            {
                if (item.Key == builder2.ToString())
                {
                    builder.Append(item.Value);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("Error");
                 //cixis kimi bir sey
            }
            
            test_n += 5;  
            builder2.Clear();
        }
        return builder.ToString();
    }
    
    return builder.ToString();
}


public static async Task FileNameDecryptAsync(string directoryPath)
    {
        if(Directory.Exists(directoryPath)){
        foreach (var file in Directory.GetFiles(directoryPath))
        {
            FileNameDecryptAsync(file);
        }
        foreach(var item in Directory.GetDirectories(directoryPath)){
        
        await FileNameDecryptAsync(item);
    }
    }
    ////
    else if(File.Exists(directoryPath)){

        string fileName = Path.GetFileName(directoryPath);
        
        string decryptedFileName = DecryptedString(fileName);
        string directoryPathH = Path.GetDirectoryName(directoryPath);
        string newFilePath = Path.Combine(directoryPathH,decryptedFileName);
        if (!File.Exists(newFilePath))
            {
                File.Move(directoryPath, newFilePath);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"File Decrypted:");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{directoryPath}  ->  {newFilePath}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"File name collision: {newFilePath} already exists. Skipping.");
            }

        
    }
    else{
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Error:file or direcotry doesnt exist as this name: {directoryPath}");
        Console.ResetColor();
    }




}
}



