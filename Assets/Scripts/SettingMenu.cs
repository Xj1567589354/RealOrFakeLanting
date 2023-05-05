using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public Dropdown resolutionDropdown;     //�ֱ��������б�
    Resolution[] resolutions;       //�ֱ�������

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();          //��ʼ����շֱ��������б�

        List<string> Options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "��" + resolutions[i].height;     //�ַ�����ʽ
            Options.Add(option);       //��ӵ��ַ����б�

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(Options);     //���ַ����б���ӵ��ֱ��������б���

        //��ʼ���Զ���⵱ǰ���Էֱ��ʲ�ˢ�·ֱ��������б���
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();     //
    }

    /// <summary>
    /// ��̬���Ļ����¼�
    /// </summary>
    /// <param name="qualityIndex">��������</param>
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    /// <summary>
    /// ����ȫ����ʾ�¼�
    /// </summary>
    /// <param name="isFullScreen">����</param>
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    /// <summary>
    /// ���·ֱ����¼�
    /// </summary>
    /// <param name="resolutionIndex">�ֱ�������</param>
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];       //��ȡ�ֱ�������������Ӧ�ķֱ���
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);       //���µ�ǰ������ֱ���
    }
}
