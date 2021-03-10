using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{

      [SerializeField] float backgroundScrollingSpeed = 0.02f;
      Material backgroundMaterial;
      Vector2 offset;

      // Start is called before the first frame update
      void Start()
      {
          backgroundMaterial = GetComponent<Renderer>().material;
          offset = new Vector2(0f, backgroundScrollingSpeed);
      }

      // Update is called once per frame
      void Update() 
      { 
          backgroundMaterial.mainTextureOffset = backgroundMaterial.mainTextureOffset + (offset * Time.deltaTime); 
      }
    /*
        public GameObject[] bg1Layer;
        public GameObject[] bg2Layer;
        public GameObject[] bg3Layer;
        [SerializeField] float[] layerSpeed;

        float bgHeight;

        [SerializeField] float offset = 1;

        private float cameraHeight;



        // Start is called before the first frame update
        void Start()
        {
            Vector3 heading = transform.position - Camera.main.transform.position;
            float distance = Vector3.Dot(heading, Camera.main.transform.forward);

            cameraHeight = 2.0f * distance * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);

            bgHeight = bg1Layer[0].GetComponent<SpriteRenderer>().bounds.size.y;
            Debug.LogError(cameraHeight);

        }

        // Update is called once per frame
        void Update()
        {
            ScrollBackground();
        }

        void ScrollBackground()
        {

            for (int i = 0; i < layerSpeed.Length; i++)
            {
                bg1Layer[i].transform.position = new Vector2(bg1Layer[i].transform.position.x , bg1Layer[i].transform.position.y - layerSpeed[i]);
                bg2Layer[i].transform.position = new Vector2(bg2Layer[i].transform.position.x , bg2Layer[i].transform.position.y - layerSpeed[i]);
                bg3Layer[i].transform.position = new Vector2(bg3Layer[i].transform.position.x , bg3Layer[i].transform.position.y - layerSpeed[i]);

                if (bg1Layer[i].transform.position.y <= -(cameraHeight + offset))
                {
                    bg1Layer[i].transform.position = new Vector2(bg1Layer[i].transform.position.x, bgHeight * 2f);
                }

                if (bg2Layer[i].transform.position.y <= -(cameraHeight + offset))
                {
                    bg2Layer[i].transform.position = new Vector2(bg2Layer[i].transform.position.x, bgHeight * 2f);
                }

                if (bg3Layer[i].transform.position.y <= -(cameraHeight + offset))
                {
                    bg3Layer[i].transform.position = new Vector2(bg3Layer[i].transform.position.x, bgHeight * 2f);
                }
            }
        }*/

 /*   float length, startpos, spriteBounds_y;
    public GameObject cam;

    public float effect, choke, moveCamSpeed;

    private void Start()
    {
        spriteBounds_y = GetComponent<SpriteRenderer>().bounds.size.y;
        transform.GetChild(0).transform.position = new Vector3(transform.position.x, spriteBounds_y, transform.position.z);
        transform.GetChild(1).transform.position = new Vector3(transform.position.x, -spriteBounds_y, transform.position.z);

        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void Update()
    {
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - moveCamSpeed, cam.transform.position.z);

        float temp = (cam.transform.position.y * (1 - effect));
        float dist = (cam.transform.position.y * effect);

        transform.position = new Vector3(transform.position.x, startpos + dist, transform.position.z);

        if (temp > startpos + length) startpos += length - choke;
        else if (temp < startpos - length) startpos -= length + choke;
    }*/
}
