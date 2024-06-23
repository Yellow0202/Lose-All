using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    private void Awake()
    {
        //Screen.SetResolution(1920, 1080, true);

        //// 현재 GameObject에 부착된 Camera 컴포넌트를 가져오는 코드
        //Camera cam = GetComponent<Camera>();

        //// 현재 카메라의 뷰포트 영역을 가져오는 코드
        //Rect viewportRect = cam.rect;

        //// 원하는 가로 세로 비율을 계산하는 코드
        //float screenAspectRatio = (float)Screen.width / Screen.height;
        //float targetAspectRatio = 16f / 9f; // 원하는 고정 비율 설정 (예: 16:9)

        //// 화면 가로 세로 비율에 따라 뷰포트 영역을 조정하는 코드
        //if (screenAspectRatio < targetAspectRatio)
        //{
        //    // 화면이 더 '높다'면 (세로가 더 길다면) 세로를 조절하는 코드
        //    viewportRect.height = screenAspectRatio / targetAspectRatio;
        //    viewportRect.y = (1f - viewportRect.height) / 2f;
        //}
        //else
        //{
        //    // 화면이 더 '넓다'면 (가로가 더 길다면) 가로를 조절하는 코드.
        //    viewportRect.width = targetAspectRatio / screenAspectRatio;
        //    viewportRect.x = (1f - viewportRect.width) / 2f;
        //}

        //// 조정된 뷰포트 영역을 카메라에 설정하는 코드
        //cam.rect = viewportRect;

        //Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //int width = 1920;
        //int height = 1080;
        //float res = (float)width / height;

        //int deviceWidth = Screen.width;
        //int deviceHeight = Screen.height;
        //float deviceRes = (float)deviceWidth / deviceHeight;

        //// SetResolution 함수 제대로 사용하기
        //Screen.SetResolution(width, (int)(((float)deviceHeight / deviceWidth) * width), true);

        //if (res < deviceRes)
        //{
        //    // 기기의 해상도 비가 더 큰 경우
        //    float newWidth = res / deviceRes;
        //    Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f);
        //}
        //else
        //{
        //    // 게임의 해상도 비가 더 큰 경우
        //    float newHeight = deviceRes / res;
        //    Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight);
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        //SetResolution(); // 초기에 게임 해상도 고정
        //setupCamera();
        OnPreCull();
    }

    private void OnPreCull()
    {
        GL.Clear(true, true, Color.black);
    }

    /* 해상도 설정하는 함수 */
    public void SetResolution()
    {
        int setWidth = 1920; // 사용자 설정 너비
        int setHeight = 1080; // 사용자 설정 높이

        int deviceWidth = Screen.width; // 기기 너비 저장
        int deviceHeight = Screen.height; // 기기 높이 저장

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution 함수 제대로 사용하기

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // 새로운 너비
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }
    }

    private void setupCamera()
    {
        //가로 화면 비율
        float targetWidthAspect = 16.0f;

        //세로 화면 비율
        float targetHeightAspect = 9.0f;

        //메인 카메라
        Camera mainCamera = Camera.main;

        mainCamera.aspect = targetWidthAspect / targetHeightAspect;

        float widthRatio = (float)Screen.width / targetWidthAspect;
        float heightRatio = (float)Screen.height / targetHeightAspect;

        float heightadd = ((widthRatio / (heightRatio / 100)) - 100) / 200;
        float widthtadd = ((heightRatio / (widthRatio / 100)) - 100) / 200;

        // 16_10비율보다 가로가 짦다면(4_3 비율)
        // 16_10비율보다 세로가 짧다면(16_9 비율)
        // 시작 지점을 0으로 만들어준다
        if (heightRatio > widthRatio)
            widthtadd = 0.0f;
        else
            heightadd = 0.0f;


        mainCamera.rect = new Rect(
            mainCamera.rect.x + Math.Abs(widthtadd),
            mainCamera.rect.y + Math.Abs(heightadd),
            mainCamera.rect.width + (widthtadd * 2),
            mainCamera.rect.height + (heightadd * 2));
    }


}
