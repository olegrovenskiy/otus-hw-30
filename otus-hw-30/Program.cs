using System.Net;



// Откуда будем качать
   string remoteUri = "https://kartinkin.net/pics/uploads/posts/2022-09/1662498989_19-kartinkin-net-p-kot-maikun-koti-21.jpg";
// Как назовем файл на диске
   string fileName = "bigimage.jpg";

//2. Создайте экземпляр этого класса

ImageDownloader myObj = new ImageDownloader(remoteUri, fileName);

//  подпишитесь на эти события

myObj.ImageStarted += DisplayMessage;

myObj.ImageCompleted += DisplayMessage;

//3. вызовите скачивание большой картинки

Console.WriteLine("Качаю \"{0}\" из \"{1}\" .......\n\n", fileName, remoteUri);

// myObj.Download();  использовалось в п. 1-3

// var isDounloaded = myObj.Download().Result;

bool ff = false;

Task.Run(() => ff = myObj.Download().Result);

Console.WriteLine("Успешно скачал \"{0}\" из \"{1}\"", fileName, remoteUri);

// обработчик событий, метод с такой же сигнатурой как и делегат
void DisplayMessage(string message) => Console.WriteLine(message);

//Console.WriteLine("Нажмите любую клавишу для завершения программы");
//Console.ReadKey();

Console.WriteLine("Нажмите клавишу A для выхода или любую другую клавишу для проверки статуса скачивания");

var choise = Console.ReadKey().KeyChar;

if (choise.Equals('A'))
    
{
    System.Environment.Exit(1);
}

else
{
   
    Console.WriteLine("Файл скачен     " + ff);
    Console.ReadKey();
}


// 1. Напишите класс ImageDownloader. В этом классе должен быть метод Download, который скачивает картинку из интернета

public class ImageDownloader : WebClient

{

    public ImageDownloader (string _remoteUri, string _fileName)
    {
        remoteUri = _remoteUri;
        fileName = _fileName;
    
    }
    public string remoteUri { get; set; }
    public string fileName { get; set; }

    // Добавьте события: в классе ImageDownloader в начале скачивания картинки и в конце скачивания

    public event Action<string> ImageStarted;
    public event Action<string> ImageCompleted;

    // метод синхронный
    /*
        public void Download ()
        {
            ImageStarted?.Invoke("Скачивание файла началось");
            DownloadFile (remoteUri, fileName);
            ImageCompleted?.Invoke("Скачивание файла закончилось");

        }

     */

    // метод асинхронный

    public async Task<bool> Download()
    {
        ImageStarted?.Invoke("Скачивание файла началось");

        Task taskDownload = DownloadFileTaskAsync(remoteUri, fileName);

        await taskDownload;

        ImageCompleted?.Invoke("Скачивание файла закончилось");

        return taskDownload.IsCompleted;
       

    }


}