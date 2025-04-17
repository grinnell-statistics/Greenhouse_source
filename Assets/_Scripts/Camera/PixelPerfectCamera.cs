using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cinemachine
{
    [DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
    [ExecuteInEditMode]
    [AddComponentMenu("")] // Hide in menu
    [SaveDuringPlay]
    public class PixelPerfectCamera : CinemachineExtension
    {
        [SerializeField]
        public float unitSize = 400;


        [SerializeField]
        private bool pixelPerfectMovement = false;


        public void Update()
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_Farmhouse"))
            {
                unitSize = 400;
            }
            else
            {
                unitSize = 200;
            }
        }


        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage == CinemachineCore.Stage.Body)
            {

                ((CinemachineVirtualCamera)vcam).m_Lens.OrthographicSize = Screen.height / (unitSize * 2.0f);

                if (pixelPerfectMovement)
                {
                    Vector3 rawPosition = state.RawPosition;
                    for (int i = 0; i < 3; i++)
                    {
                        rawPosition[i] = (Mathf.Ceil(rawPosition[i] * unitSize) / unitSize);
                    }
                    state.RawPosition = rawPosition;
                }

            }
        }

        int GetNearestMultiple(int value, int multiple)
        {
            int remainder = value % multiple;

            int result = value - remainder;

            if (remainder > (multiple / 2))
            {
                result += multiple;
            }

            return result;
        }
    }
}