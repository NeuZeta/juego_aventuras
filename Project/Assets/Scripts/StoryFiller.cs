using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryFiller : MonoBehaviour
{
  
   

    public static GameplayManager.StoryNode FillStory() {

        bool item1 = false;
        bool item2 = false;
        bool item3 = false;

        GameplayManager.StoryNode nodoRaíz = CreateNode 
            ("Te despiertas en una habitación desconocida, no recuerdas nada y te duele la cabeza. Quieres salir", 
            new string[] {
                "Sentarte a llorar",
                "Explorar habitación"
            });

            GameplayManager.StoryNode nodo1A = CreateNode
                ("Después de un rato llorando, sólo consigues que te duela más la cabeza.",
                new string[] {
                    "Sentarte a llorar",
                    "Explorar habitación"
                });
            nodoRaíz.nextNode[0] = nodo1A;
            nodo1A.nextNode[0] = nodo1A;
        

            GameplayManager.StoryNode nodo1B = CreateNode
                ("Es una habitación infantil. Hay una puerta, varias fotografías colgadas en la pared, una cama pequeña con cojines y una caja fuerte de combinación.",
                new string[] {
                    "Mirar fotografías",
                    "Examinar caja fuerte",
                    "Examinar cama",
                    "Abrir puerta"
                });
            nodoRaíz.nextNode[1] = nodo1B;
            nodo1A.nextNode[1] = nodo1B;
            

        GameplayManager.StoryNode nodo2A = CreateNode
                   ("Fotografías de un grupo de niñas. Una de las fotografías destaca sobre las demás, parece antigüa",
                   new string[] {
                        "Examinar fotografía antigüa",
                        "Dejar las fotografías y volver al resto de objetos"
                   });
                nodo1B.nextNode[0] = nodo2A;
                nodo2A.nextNode[1] = nodo1B;

                    GameplayManager.StoryNode nodo3A = CreateNode
                       ("Al observar la fotografía, ves que tiene el año escrito a mano en la parte inferior derecha: 1984. Este año parece importante",
                       new string[] {
                            "Volver al resto de objetos de la habitación"
                       });
                    nodo2A.nextNode[0] = nodo3A;
                    nodo3A.nextNode[0] = nodo1B;
                    nodo3A.foundPicture = () => { item1 = true;};



                GameplayManager.StoryNode nodo2B = CreateNode
                   ("Es una caja con un candado de combinación de 4 dígitos...",
                   new string[] {
                        "Probar combinación al azar",
                        "Dejar la caja y volver al resto de objetos"
                   });
                nodo1B.nextNode[1] = nodo2B;
                nodo2B.nextNode[1] = nodo1B;
 
                    GameplayManager.StoryNode nodo3B = CreateNode
                       ("Pruebas la combinación 1-1-1-1, nunca fuiste muy original. Sorpresa... no funciona",
                       new string[] {
                            "Volver al resto de objetos de la habitación"
                       });
                    nodo2B.nextNode[0] = nodo3B;
                    nodo3B.nextNode[0] = nodo1B;


                GameplayManager.StoryNode nodo2B_bis = CreateNode
                  ("Es una caja con un candado de combinación de 4 dígitos...",
                  new string[] {
                        "Probar combinación 1-9-8-4",
                        "Dejar la caja y volver al resto de objetos"
                  });
        
                nodo3A.nodeVisited = () =>
                {
                    nodo1B.nextNode[1] = nodo2B_bis;
                    nodo2B_bis.nextNode[1] = nodo1B;
                };

                    GameplayManager.StoryNode nodo3C = CreateNode
                        ("Has abierto la caja fuerte! Dentro hay una llave... ¿Qué abrirá?",
                        new string[] {
                            "Volver al resto de objetos de la habitación"
                        });
                    nodo2B_bis.nextNode[0] = nodo3C;
                    nodo3C.nextNode[0] = nodo1B;
                    nodo3C.foundKey = () => { item2 = true; };

        GameplayManager.StoryNode nodo2C = CreateNode
                 ("Es una cama de niña, con muchos cojines. Parece cómoda... Hay un osito de peluche que te trae recuerdos de la infancia. No puedes evitar quedártelo.",
                 new string[] {
                    "Volver al resto de objetos de la habitación"
                 });
            nodo1B.nextNode[2] = nodo2C;
            nodo2C.nextNode[0] = nodo1B;
            nodo2C.foundTeddybear = () => { item3 = true; };

        GameplayManager.StoryNode nodo2D = CreateNode
                  ("La puerta está cerrada con llave, no consigues nada",
                  new string[] {
                    "Volver al resto de objetos de la habitación"
                  });
            nodo1B.nextNode[3] = nodo2D;
            nodo2D.nextNode[0] = nodo1B;

        GameplayManager.StoryNode nodo2D_bis = CreateNode
                ("¡¡Has salido de la habitación!!",
                new string[] {
                    
                });

            nodo3C.nodeVisited = () =>
            {
                nodo1B.nextNode[3] = nodo2D_bis;
                nodo2D_bis.isFinal = true;
            };

        return nodoRaíz;

    }

   static GameplayManager.StoryNode CreateNode (string texto, string[] respuestas)
    {
        GameplayManager.StoryNode nuevoNodo = new GameplayManager.StoryNode();
        nuevoNodo.story = texto;
        nuevoNodo.answers = respuestas;
        nuevoNodo.nextNode = new GameplayManager.StoryNode[10];
        return nuevoNodo;
    }
}
