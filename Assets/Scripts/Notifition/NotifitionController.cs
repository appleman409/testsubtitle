using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifitionController : MonoBehaviour
{
    [System.Serializable]
    public struct Notifition
    {
        public string title;
        public Vector3 position;
    }
    public List<Notifition> notifs = new List<Notifition>();
    public GameObject prefab;

    private void Start()
    {
        for (int i = 0; i < notifs.Count; i++)
        {
            var noti = Instantiate(prefab, gameObject.transform);
            var noticom = noti.GetComponent<Notifition>();
            noticom.title = notifs[i].title;
            noticom.position = notifs[i].position;
        }
    }
}
