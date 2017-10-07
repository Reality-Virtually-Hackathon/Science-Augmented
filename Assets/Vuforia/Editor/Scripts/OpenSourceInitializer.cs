/*===============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Confidential and Proprietary - Protected under copyright and other laws.
Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/

using System.Linq;
using UnityEditor;
using UnityEngine;
#if!UNITY_WEBGL
using Vuforia;
using Vuforia.EditorClasses;
#endif

/// <summary>
/// Creates connection between open source files and the Vuforia library.
/// Do not modify.
/// </summary>
[InitializeOnLoad]
public static class OpenSourceInitializer
{
	#if!UNITY_WEBGL
    static OpenSourceInitializer()
    {
        GameObjectFactory.SetDefaultBehaviourTypeConfiguration(new DefaultBehaviourAttacher());
        ReplacePlaceHolders();
    }

    static void ReplacePlaceHolders()
    {
        var trackablePlaceholders = Object.FindObjectsOfType<DefaultTrackableBehaviourPlaceholder>().ToList();
        var smartTerrainPlaceholders = Object.FindObjectsOfType<DefaultSmartTerrainEventHandlerPlaceHolder>().ToList();
        var initErrorsPlaceholders = Object.FindObjectsOfType<DefaultInitializationErrorHandlerPlaceHolder>().ToList();
        
        trackablePlaceholders.ForEach(ReplaceTrackablePlaceHolder);
        smartTerrainPlaceholders.ForEach(ReplaceSmartTerrainPlaceHolder);
        initErrorsPlaceholders.ForEach(ReplaceInitErrorPlaceHolder);
    }
    
    static void ReplaceTrackablePlaceHolder(DefaultTrackableBehaviourPlaceholder placeHolder)
    {
        var go = placeHolder.gameObject;
        go.AddComponent<DefaultTrackableEventHandler>();

        Object.DestroyImmediate(placeHolder);
    }

    static void ReplaceSmartTerrainPlaceHolder(DefaultSmartTerrainEventHandlerPlaceHolder placeHolder)
    {
        var go = placeHolder.gameObject;
        var eventHandler = go.AddComponent<DefaultSmartTerrainEventHandler>();
        eventHandler.PropTemplate = placeHolder.PropBehaviour;
        eventHandler.SurfaceTemplate = placeHolder.SurfaceBehaviour;

        Object.DestroyImmediate(placeHolder);
    }

    static void ReplaceInitErrorPlaceHolder(DefaultInitializationErrorHandlerPlaceHolder placeHolder)
    {
        var go = placeHolder.gameObject;
        go.AddComponent<DefaultInitializationErrorHandler>();

        Object.DestroyImmediate(placeHolder);
    }

    class DefaultBehaviourAttacher : IDefaultBehaviourAttacher
    {
        public void AddDefaultTrackableBehaviour(GameObject go)
        {
            go.AddComponent<DefaultTrackableEventHandler>();
        }

        public void AddDefaultSmartTerrainEventHandler(GameObject go, PropBehaviour prop,
            SurfaceBehaviour primarySurface)
        {
            var handler = go.AddComponent<DefaultSmartTerrainEventHandler>();
            handler.PropTemplate = prop.GetComponent<PropBehaviour>();
            handler.SurfaceTemplate = primarySurface.GetComponent<SurfaceBehaviour>();
        }

        public void AddDefaultInitializationErrorHandler(GameObject go)
        {
            go.AddComponent<DefaultInitializationErrorHandler>();
        }
    }
	#endif
}
