using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public Dropdown resolutionDropdown;     //分辨率下拉列表
    Resolution[] resolutions;       //分辨率数组

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();          //初始化清空分辨率下拉列表

        List<string> Options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "×" + resolutions[i].height;     //字符串格式
            Options.Add(option);       //添加到字符串列表

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(Options);     //将字符串列表添加到分辨率下拉列表中

        //初始化自动检测当前电脑分辨率并刷新分辨率下拉列表中
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();     //
    }

    /// <summary>
    /// 动态更改画质事件
    /// </summary>
    /// <param name="qualityIndex">画质索引</param>
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    /// <summary>
    /// 设置全屏显示事件
    /// </summary>
    /// <param name="isFullScreen">开关</param>
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    /// <summary>
    /// 更新分辨率事件
    /// </summary>
    /// <param name="resolutionIndex">分辨率索引</param>
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];       //获取分辨率数组索引对应的分辨率
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);       //更新当前计算机分辨率
    }
}
