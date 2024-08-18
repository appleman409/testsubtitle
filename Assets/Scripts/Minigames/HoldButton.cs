using UnityEngine;
using RenderHeads.Media.AVProVideo;
using UnityEngine.UI;
using System.Collections;

public class HoldButton : MonoBehaviour
{
    public MediaPlayer mediaPlayer;
    public Button button;            
    private float initialTime;
    public float offset;
    public bool isdone = false;
    public bool isRewinding = false;
    public bool isDragging = false;
    public float speed = 2f;

    void Start()
    {
        mediaPlayer.Pause();
        // Lưu lại thời gian ban đầu của video
        initialTime = mediaPlayer.Control.GetCurrentTimeMs();

        // Add Listener cho button để nhận sự kiện kéo
        button.onClick.AddListener(() => { OnButtonClick(); });
    }

    void Update()
    {
        
        if (!isdone)
        {
            if (mediaPlayer.Control.IsPlaying()) mediaPlayer.Pause();
            if (!isDragging && isRewinding)
            {
                float currentTime = mediaPlayer.Control.GetCurrentTimeMs();
                currentTime -= speed;
                mediaPlayer.Control.Seek(currentTime);

            }

            // Kiểm tra nếu người dùng đang giữ và kéo button
            if (Input.GetMouseButton(0))
            {
                if (!isdone && isDragging)
                {
                    StopAllCoroutines(); // Ngừng quá trình tua ngược nếu có
                    isRewinding = false;
                    // Tính toán thời gian video theo vị trí kéo của button
                    float newTime = Mathf.Lerp(initialTime, mediaPlayer.Info.GetDurationMs(), Input.mousePosition.y / Screen.height);
                    Debug.Log(newTime);
                    mediaPlayer.Control.Seek(newTime - offset);
                }
            }
        }
    }

    public void OnButtonClick()
    {
        // Chỉ bắt đầu kéo nếu video đang tạm dừng
        if (!mediaPlayer.Control.IsPlaying())
        {
            isDragging = true;
            isRewinding = false;
        }
    }

    public void OnButtonRelease()
    {
        // Khi nút được thả, dừng việc kéo và bắt đầu tua ngược
        isDragging = false;
        if (!isRewinding)
        {
            if (mediaPlayer.Control.GetCurrentTimeMs() > 3000f)
            {
                isdone = true;
                mediaPlayer.Play();
                button.gameObject.SetActive(false);
                Destroy(gameObject);
            }
            isRewinding = true;
        }
    }
}
