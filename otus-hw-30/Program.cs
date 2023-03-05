using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;



var siteList = new List<string>()
{
"https://fikiwiki.com/uploads/posts/2022-02/1645000472_12-fikiwiki-com-p-kartinki-krasivie-na-telefon-zhivie-oboi-13.jpg",
"https://fikiwiki.com/uploads/posts/2022-02/1645000472_12-fikiwiki-com-p-kartinki-krasivie-na-telefon-zhivie-oboi-13.jpg",
"https://fikiwiki.com/uploads/posts/2022-02/1645000472_12-fikiwiki-com-p-kartinki-krasivie-na-telefon-zhivie-oboi-13.jpg",
"https://fikiwiki.com/uploads/posts/2022-02/1645000472_12-fikiwiki-com-p-kartinki-krasivie-na-telefon-zhivie-oboi-13.jpg",
"https://fikiwiki.com/uploads/posts/2022-02/1645000472_12-fikiwiki-com-p-kartinki-krasivie-na-telefon-zhivie-oboi-13.jpg",
"https://fikiwiki.com/uploads/posts/2022-02/1645000472_12-fikiwiki-com-p-kartinki-krasivie-na-telefon-zhivie-oboi-13.jpg",
"https://fikiwiki.com/uploads/posts/2022-02/1645000472_12-fikiwiki-com-p-kartinki-krasivie-na-telefon-zhivie-oboi-13.jpg",
"https://fikiwiki.com/uploads/posts/2022-02/1645000472_12-fikiwiki-com-p-kartinki-krasivie-na-telefon-zhivie-oboi-13.jpg",
"https://fikiwiki.com/uploads/posts/2022-02/1645000472_12-fikiwiki-com-p-kartinki-krasivie-na-telefon-zhivie-oboi-13.jpg",
"https://fikiwiki.com/uploads/posts/2022-02/1645000472_12-fikiwiki-com-p-kartinki-krasivie-na-telefon-zhivie-oboi-13.jpg"

};


var fileList = new List<string>()
{
    "1.jpg",
    "2.jpg",
    "3.jpg",
    "4.jpg",
    "5.jpg",
    "6.jpg",
    "7.jpg",
    "8.jpg",
    "9.jpg",
    "10.jpg"
   };


var taskList = new List<Task>();


CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
CancellationToken token = cancelTokenSource.Token;

int i = 0;

foreach (var site in siteList)
{
    string fileName = fileList[i];
    var task = Task.Run(async () => {new ImageDownloader(site, fileName).Download();}, token);
    taskList.Add(task);
    
    i++;
}

Console.WriteLine("Нажмите клавишу A для прекращения загрузки или любую другую клавишу для проверки статуса скачивания");

var choise = Console.ReadKey().KeyChar;


if (choise.Equals('A'))
    
{
    cancelTokenSource.Cancel();
    Console.WriteLine("Скачивание отменено");
    Console.ReadKey();
}

else
{
    i = 0;

    foreach (var task in taskList)
    {
        string fileName = fileList[i];
        Console.WriteLine($"Файл {fileName} скачен     " + task.IsCompleted);
        i++;
    }
    Console.ReadKey();

}

public class ImageDownloader : WebClient

{

    public ImageDownloader (string _remoteUri, string _fileName)
    {
        remoteUri = _remoteUri;
        fileName = _fileName;
    
    }
    public string remoteUri { get; set; }
    public string fileName { get; set; }

        public event Action<string> ImageStarted;
    public event Action<string> ImageCompleted;


    public async Task Download()
    {
        ImageStarted?.Invoke("Скачивание файла началось");

        Task taskDownload = DownloadFileTaskAsync(remoteUri, fileName);

        await taskDownload;

        ImageCompleted?.Invoke("Скачивание файла закончилось");
      
    }
}