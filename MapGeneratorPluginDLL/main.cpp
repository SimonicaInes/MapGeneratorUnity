//unity interface
#include "IUnityGraphics.h"
#include "IUnityInterface.h"


// ------------------------------------------------------------------------
// Plugin itself


// Link following functions C-style (reqired for plugins)
extern "C" 
{


   





    void UNITY_INTERFACE_EXPORT UNITY_INTERFACE_API UnityPluginLoad(IUnityInterfaces* unityInterfaces)
    {
         IUnityGraphics* graphics = unityInterfaces->Get<IUnityGraphics>();
    }

    void UNITY_INTERFACE_EXPORT UNITY_INTERFACE_API UnityPluginUnload()
    {

    }
}