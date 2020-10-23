using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace rulesencyclopediaclient.Tools
{

    class APIConnect
    {
        //Static fordi HttpClient er multithreaded og threadsafe og kan derfor håndtere kald fra flere steder i applikationen.
        //Derfor er der ikke brug for flere instanser af klassen. Derfor kan man spare ressourcer og håndtere den åbne instans
        //med using i DAO'erne
        public static HttpClient client { get; set; }


        public static void initialiseClient()
        {
            string apiAddress = "localhost";
            string portNr = "44378";
            client = new HttpClient();
            //Vi vil kun have json objekter.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string uriString = "https://" + apiAddress + ":" + portNr + "/";
            //her sætter vi addressen og porten for API servicen
            client.BaseAddress = new Uri(uriString);
            
        }


    }
}
