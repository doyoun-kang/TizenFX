/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using NativeCapabilities = Interop.CameraCapabilities;
using NativeSettings = Interop.CameraSettings;

namespace Tizen.Multimedia
{
    /// <summary>
    /// The CameraCapabilities class provides properties
    /// to get various capability information of the camera device.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class CameraCapabilities
    {
        internal readonly Camera _camera;

        private IList<Size> _previewResolutions;
        private IList<Size> _cameraResolutions;
        private IList<CameraPixelFormat> _captureFormats;
        private IList<CameraPixelFormat> _previewFormats;
        private IList<CameraFps> _fps;
        private IList<CameraAutoFocusMode> _autoFocusModes;
        private IList<CameraExposureMode> _exposureModes;
        private IList<CameraIsoLevel> _isoLevels;
        private IList<CameraTheaterMode> _theaterModes;
        private IList<CameraWhiteBalance> _whitebalances;
        private IList<CameraFlashMode> _flashModes;
        private IList<CameraSceneMode> _sceneModes;
        private IList<CameraEffectMode> _effectModes;
        private IList<Rotation> _streamRotations;
        private IList<Flips> _streamFlips;
        private IList<CameraPtzType> _ptzTypes;
        private delegate CameraError GetRangeDelegate(IntPtr handle, out int min, out int max);
        private delegate bool IsSupportedDelegate(IntPtr handle);

        internal CameraCapabilities(Camera camera)
        {
            _camera = camera;

            IsFaceDetectionSupported = IsFeatureSupported(NativeCapabilities.IsFaceDetectionSupported);
            IsMediaPacketPreviewCallbackSupported = IsFeatureSupported(NativeCapabilities.IsMediaPacketPreviewCallbackSupported);
            IsZeroShutterLagSupported = IsFeatureSupported(NativeCapabilities.IsZeroShutterLagSupported);
            IsContinuousCaptureSupported = IsFeatureSupported(NativeCapabilities.IsContinuousCaptureSupported);
            IsExtraPreviewSupported = IsFeatureSupported(NativeCapabilities.IsExtraPreviewSupported);
            IsHdrCaptureSupported = IsFeatureSupported(NativeCapabilities.IsHdrCaptureSupported);
            IsAntiShakeSupported = IsFeatureSupported(NativeCapabilities.IsAntiShakeSupported);
            IsVideoStabilizationSupported = IsFeatureSupported(NativeCapabilities.IsVideoStabilizationSupported);
            IsAutoContrastSupported = IsFeatureSupported(NativeCapabilities.IsAutoContrastSupported);
            IsBrigtnessSupported = CheckRangeValid(NativeSettings.GetBrightnessRange);
            IsExposureSupported = CheckRangeValid(NativeSettings.GetExposureRange);
            IsZoomSupported = CheckRangeValid(NativeSettings.GetZoomRange);
            IsPanSupported = CheckRangeValid(NativeSettings.GetPanRange);
            IsTiltSupported = CheckRangeValid(NativeSettings.GetTiltRange);
            IsHueSupported = CheckRangeValid(NativeSettings.GetHueRange);
            IsSaturationSupported = CheckRangeValid(NativeSettings.GetSaturationRange);
            IsSharpnessSupported = CheckRangeValid(NativeSettings.GetSharpnessRange);
            IsGainSupported = CheckRangeValid(NativeSettings.GetGainRange);
            IsWhiteBalanceTemperatureSupported = CheckRangeValid(NativeSettings.GetWhiteBalanceTemperatureRange);
        }

        private bool IsFeatureSupported(IsSupportedDelegate func)
        {
            return func(_camera.GetHandle());
        }

        private bool CheckRangeValid(GetRangeDelegate func)
        {
            func(_camera.GetHandle(), out int min, out int max).
                ThrowIfFailed("Failed to check feature is suported or not.");

            return min < max;
        }

        /// <summary>
        /// Gets the face detection feature support state.
        /// </summary>
        /// <value>true if supported, otherwise false.</value>
        /// <since_tizen> 4 </since_tizen>
        public bool IsFaceDetectionSupported { get; }

        /// <summary>
        /// Gets the media packet preview callback feature support state.
        /// </summary>
        /// <value>true if supported, otherwise false.</value>
        /// <since_tizen> 4 </since_tizen>
        public bool IsMediaPacketPreviewCallbackSupported { get; }

        /// <summary>
        /// Gets the zero shutter lag feature support state.
        /// </summary>
        /// <value>true if supported, otherwise false.</value>
        /// <since_tizen> 4 </since_tizen>
        public bool IsZeroShutterLagSupported { get; }

        /// <summary>
        /// Gets the continuous capture feature support state.
        /// </summary>
        /// <value>true if supported, otherwise false.</value>
        /// <since_tizen> 4 </since_tizen>
        public bool IsContinuousCaptureSupported { get; }

        /// <summary>
        /// Gets the extra preview feature support state.
        /// </summary>
        /// <value>true if supported, otherwise false.</value>
        /// <since_tizen> 10 </since_tizen>
        public bool IsExtraPreviewSupported { get; }

        /// <summary>
        /// Gets the HDR capture feature support state.
        /// </summary>
        /// <value>true if supported, otherwise false.</value>
        /// <since_tizen> 4 </since_tizen>
        public bool IsHdrCaptureSupported { get; }

        /// <summary>
        /// Gets the anti-shake feature feature support state.
        /// </summary>
        /// <value>true if supported, otherwise false.</value>
        /// <since_tizen> 4 </since_tizen>
        public bool IsAntiShakeSupported { get; }

        /// <summary>
        /// Gets the video stabilization feature support state.
        /// </summary>
        /// <value>true if supported, otherwise false.</value>
        /// <since_tizen> 4 </since_tizen>
        public bool IsVideoStabilizationSupported { get; }

        /// <summary>
        /// Gets the auto contrast feature support state.
        /// </summary>
        /// <value>true if supported, otherwise false.</value>
        /// <since_tizen> 4 </since_tizen>
        public bool IsAutoContrastSupported { get; }

        /// <summary>
        /// Gets the brightness feature support state.
        /// </summary>
        /// <value>true if supported, otherwise false.</value>
        /// <since_tizen> 4 </since_tizen>
        public bool IsBrigtnessSupported { get; }

        /// <summary>
        /// Gets the exposure feature support state.
        /// </summary>
        /// <value>true if supported, otherwise false.</value>
        /// <since_tizen> 4 </since_tizen>
        public bool IsExposureSupported { get; }

        /// <summary>
        /// Gets the zoom feature support state.
        /// </summary>
        /// <value>true if supported, otherwise false.</value>
        /// <since_tizen> 4 </since_tizen>
        public bool IsZoomSupported { get; }

        /// <summary>
        /// Gets the pan feature support state.
        /// </summary>
        /// <value>true if supported, otherwise false.</value>
        /// <since_tizen> 4 </since_tizen>
        public bool IsPanSupported { get; }

        /// <summary>
        /// Gets the tilt feature support state.
        /// </summary>
        /// <value>true if supported, otherwise false.</value>
        /// <since_tizen> 4 </since_tizen>
        public bool IsTiltSupported { get; }

        /// <summary>
        /// Gets the hue feature support state.
        /// </summary>
        /// <value>true if supported, otherwise false.</value>
        /// <since_tizen> 5 </since_tizen>
        public bool IsHueSupported { get; }

        /// <summary>
        /// Gets the saturation feature support state.
        /// </summary>
        /// <value>true if supported, otherwise false.</value>
        /// <since_tizen> 11 </since_tizen>
        public bool IsSaturationSupported { get; }

        /// <summary>
        /// Gets the sharpness feature support state.
        /// </summary>
        /// <value>true if supported, otherwise false.</value>
        /// <since_tizen> 11 </since_tizen>
        public bool IsSharpnessSupported { get; }

        /// <summary>
        /// Gets the gain feature support state.
        /// </summary>
        /// <value>true if supported, otherwise false.</value>
        /// <since_tizen> 11 </since_tizen>
        public bool IsGainSupported { get; }

        /// <summary>
        /// Gets the white balance temperature feature support state.
        /// </summary>
        /// <value>true if supported, otherwise false.</value>
        /// <since_tizen> 11 </since_tizen>
        public bool IsWhiteBalanceTemperatureSupported { get; }

        /// <summary>
        /// Retrieves all the preview resolutions supported by the camera.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        /// <returns>
        /// It returns a list containing all the supported preview resolutions.
        /// </returns>
        /// <exception cref="ObjectDisposedException">The camera has already been disposed. </exception>
        public IEnumerable<Size> SupportedPreviewResolutions
        {
            get
            {
                _camera.ValidateNotDisposed();

                if (_previewResolutions == null)
                {
                    _previewResolutions = GetSupportedPreviewResolutions();
                }

                return _previewResolutions;
            }
        }

        /// <summary>
        /// Retrieves all the capture resolutions supported by the camera.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        /// <returns>
        /// It returns a list containing all the supported capture resolutions.
        /// </returns>
        /// <exception cref="ObjectDisposedException">The camera has already been disposed. </exception>
        public IEnumerable<Size> SupportedCaptureResolutions
        {
            get
            {
                _camera.ValidateNotDisposed();

                if (_cameraResolutions == null)
                {
                    _cameraResolutions = GetSupportedCaptureResolutions();
                }

                return _cameraResolutions;
            }
        }

        /// <summary>
        /// Retrieves all the capture formats supported by the camera.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        /// <returns>
        /// It returns a list containing all the supported <see cref="CameraPixelFormat"/>.
        /// </returns>
        /// <exception cref="ObjectDisposedException">The camera has already been disposed. </exception>
        public IEnumerable<CameraPixelFormat> SupportedCapturePixelFormats
        {
            get
            {
                _camera.ValidateNotDisposed();

                if (_captureFormats == null)
                {
                    _captureFormats = GetSupportedCapturePixelFormats();
                }

                return _captureFormats;
            }
        }

        /// <summary>
        /// Retrieves all the preview formats supported by the camera.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        /// <returns>
        /// It returns a list containing all the supported <see cref="CameraPixelFormat"/>.
        /// </returns>
        /// <exception cref="ObjectDisposedException">The camera has already been disposed. </exception>
        public IEnumerable<CameraPixelFormat> SupportedPreviewPixelFormats
        {
            get
            {
                _camera.ValidateNotDisposed();

                if (_previewFormats == null)
                {
                    _previewFormats = GetSupportedPreviewPixelFormats();
                }

                return _previewFormats;
            }
        }

        /// <summary>
        /// Retrieves all the fps supported by the camera.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        /// <returns>
        /// It returns a list containing all the supported <see cref="CameraFps"/>.
        /// </returns>
        /// <exception cref="ObjectDisposedException">The camera has already been disposed. </exception>
        public IEnumerable<CameraFps> SupportedPreviewFps
        {
            get
            {
                _camera.ValidateNotDisposed();

                if (_fps == null)
                {
                    _fps = GetSupportedPreviewFps();
                }

                return _fps;
            }
        }

        /// <summary>
        /// Retrieves all the fps by resolution supported by the camera.
        /// </summary>
        /// <param name="width">The width of required preview resolution.</param>
        /// <param name="height">The height of required preview resolution.</param>
        /// <returns>
        /// It returns a list containing all the supported <see cref="CameraFps"/> by resolution.
        /// </returns>
        /// <since_tizen> 4 </since_tizen>
        /// <feature> http://tizen.org/feature/camera </feature>
        /// <exception cref="ObjectDisposedException">The camera has already been disposed. </exception>
        public IEnumerable<CameraFps> GetSupportedPreviewFpsByResolution(int width, int height)
        {
            return GetSupportedPreviewFpsByResolutions(width, height);
        }

        /// <summary>
        /// Retrieves all the fps by resolution supported by the camera.
        /// </summary>
        /// <param name="size">The size of required preview resolution.</param>
        /// <returns>
        /// It returns a list containing all the supported <see cref="CameraFps"/> by resolution.
        /// </returns>
        /// <since_tizen> 4 </since_tizen>
        /// <feature> http://tizen.org/feature/camera </feature>
        /// <exception cref="ObjectDisposedException">The camera has already been disposed. </exception>
        public IEnumerable<CameraFps> GetSupportedPreviewFpsByResolution(Size size)
        {
            return GetSupportedPreviewFpsByResolutions(size.Width, size.Height);
        }

        /// <summary>
        /// Retrieves all the auto focus modes supported by the camera.
        /// </summary>
        /// <returns>
        /// It returns a list containing all the supported <see cref="CameraAutoFocusMode"/>.
        /// </returns>
        /// <since_tizen> 4 </since_tizen>
        /// <exception cref="ObjectDisposedException">The camera has already been disposed. </exception>
        public IEnumerable<CameraAutoFocusMode> SupportedAutoFocusModes
        {
            get
            {
                _camera.ValidateNotDisposed();

                if (_autoFocusModes == null)
                {
                    _autoFocusModes = GetSupportedAutoFocusModes();
                }

                return _autoFocusModes;
            }
        }

        /// <summary>
        /// Retrieves all the exposure modes supported by the camera.
        /// </summary>
        /// <returns>
        /// It returns a list containing all the supported <see cref="CameraExposureMode"/>.
        /// </returns>
        /// <since_tizen> 4 </since_tizen>
        /// <exception cref="ObjectDisposedException">The camera has already been disposed. </exception>
        public IEnumerable<CameraExposureMode> SupportedExposureModes
        {
            get
            {
                _camera.ValidateNotDisposed();

                if (_exposureModes == null)
                {
                    _exposureModes = GetSupportedExposureModes();
                }

                return _exposureModes;
            }
        }

        /// <summary>
        /// Retrieves all the ISO levels supported by the camera.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        /// <returns>
        /// It returns a list containing all the supported <see cref="CameraIsoLevel"/>.
        /// </returns>
        /// <exception cref="ObjectDisposedException">The camera has already been disposed. </exception>
        public IEnumerable<CameraIsoLevel> SupportedIsoLevels
        {
            get
            {
                _camera.ValidateNotDisposed();

                if (_isoLevels == null)
                {
                    _isoLevels = GetSupportedIsoLevels();
                }

                return _isoLevels;
            }
        }

        /// <summary>
        /// Retrieves all the theater modes supported by the camera.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        /// <returns>
        /// It returns a list containing all the supported <see cref="CameraTheaterMode"/>.
        /// </returns>
        /// <exception cref="ObjectDisposedException">The camera has already been disposed. </exception>
        public IEnumerable<CameraTheaterMode> SupportedTheaterModes
        {
            get
            {
                _camera.ValidateNotDisposed();

                if (_theaterModes == null)
                {
                    _theaterModes = GetSupportedTheaterModes();
                }

                return _theaterModes;
            }
        }

        /// <summary>
        /// Retrieves all the white balance modes supported by the camera.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        /// <returns>
        /// It returns a list containing all the supported <see cref="CameraWhiteBalance"/>.
        /// </returns>
        /// <exception cref="ObjectDisposedException">The camera has already been disposed. </exception>
        public IEnumerable<CameraWhiteBalance> SupportedWhiteBalances
        {
            get
            {
                _camera.ValidateNotDisposed();

                if (_whitebalances == null)
                {
                    _whitebalances = GetSupportedWhitebalances();
                }

                return _whitebalances;
            }
        }

        /// <summary>
        /// Retrieves all the flash modes supported by the camera.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        /// <returns>
        /// It returns a list containing all the supported <see cref="CameraFlashMode"/>.
        /// </returns>
        /// <exception cref="ObjectDisposedException">The camera has already been disposed. </exception>
        public IEnumerable<CameraFlashMode> SupportedFlashModes
        {
            get
            {
                _camera.ValidateNotDisposed();

                if (_flashModes == null)
                {
                    _flashModes = GetSupportedFlashModes();
                }

                return _flashModes;
            }
        }

        /// <summary>
        /// Retrieves all the scene modes supported by the camera.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        /// <returns>
        /// It returns a list containing all the supported <see cref="CameraSceneMode"/>.
        /// </returns>
        /// <exception cref="ObjectDisposedException">The camera has already been disposed. </exception>
        public IEnumerable<CameraSceneMode> SupportedSceneModes
        {
            get
            {
                _camera.ValidateNotDisposed();

                if (_sceneModes == null)
                {
                    _sceneModes = GetSupportedSceneModes();
                }

                return _sceneModes;
            }
        }

        /// <summary>
        /// Retrieves all the effect modes supported by the camera.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        /// <returns>
        /// It returns a list containing all the supported <see cref="CameraEffectMode"/>.
        /// </returns>
        /// <exception cref="ObjectDisposedException">The camera has already been disposed. </exception>
        public IEnumerable<CameraEffectMode> SupportedEffects
        {
            get
            {
                _camera.ValidateNotDisposed();

                if (_effectModes == null)
                {
                    _effectModes = GetSupportedEffects();
                }

                return _effectModes;
            }
        }

        /// <summary>
        /// Retrieves all the stream rotations supported by the camera.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        /// <returns>
        /// An IEnumerable containing all the supported <see cref="Rotation"/>.
        /// </returns>
        /// <exception cref="ObjectDisposedException">The camera has already been disposed. </exception>
        public IEnumerable<Rotation> SupportedStreamRotations
        {
            get
            {
                _camera.ValidateNotDisposed();

                if (_streamRotations == null)
                {
                    _streamRotations = GetSupportedStreamRotations();
                }

                return _streamRotations;
            }
        }

        /// <summary>
        /// Retrieves all the flips supported by the camera.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        /// <returns>
        /// It returns a list containing all the supported <see cref="Flips"/>.
        /// </returns>
        /// <exception cref="ObjectDisposedException">The camera has already been disposed. </exception>
        public IEnumerable<Flips> SupportedStreamFlips
        {
            get
            {
                _camera.ValidateNotDisposed();

                if (_streamFlips == null)
                {
                    _streamFlips = GetSupportedStreamFlips();
                }

                return _streamFlips;
            }
        }

        /// <summary>
        /// Retrieves all the PTZ types by the camera.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        /// <returns>
        /// It returns a list containing all the supported <see cref="CameraPtzType"/>.
        /// </returns>
        /// <exception cref="ObjectDisposedException">The camera has already been disposed. </exception>
        public IEnumerable<CameraPtzType> SupportedPtzTypes
        {
            get
            {
                _camera.ValidateNotDisposed();

                if (_ptzTypes == null)
                {
                    _ptzTypes = GetSupportedPtzTypes();
                }

                return _ptzTypes;
            }
        }

        #region Methods for getting supported values
        private IList<Size> GetSupportedPreviewResolutions()
        {
            var previewResolutions = new List<Size>();

            NativeCapabilities.PreviewResolutionCallback callback = (width, height, _) =>
            {
                previewResolutions.Add(new Size(width, height));
                return true;
            };

            NativeCapabilities.SupportedPreviewResolutions(_camera.GetHandle(), callback).
                ThrowIfFailed("Failed to get the supported preview resolutions");

            return previewResolutions.AsReadOnly();
        }

        private IList<Size> GetSupportedCaptureResolutions()
        {
            var cameraResolutions = new List<Size>();

            NativeCapabilities.CaptureResolutionCallback callback = (width, height, _) =>
            {
                cameraResolutions.Add(new Size(width, height));
                return true;
            };

            NativeCapabilities.SupportedCaptureResolutions(_camera.GetHandle(), callback).
                ThrowIfFailed("Failed to get the supported capture resolutions");

            return cameraResolutions.AsReadOnly();
        }

        private IList<CameraPixelFormat> GetSupportedCapturePixelFormats()
        {
            var captureFormats = new List<CameraPixelFormat>();

            NativeCapabilities.CaptureFormatCallback callback = (format, _) =>
            {
                captureFormats.Add(format);
                return true;
            };

            NativeCapabilities.SupportedCapturePixelFormats(_camera.GetHandle(), callback).
                ThrowIfFailed("Failed to get the supported capture formats.");

            return captureFormats.AsReadOnly();
        }

        private IList<CameraPixelFormat> GetSupportedPreviewPixelFormats()
        {
            var previewFormats = new List<CameraPixelFormat>();

            NativeCapabilities.PreviewFormatCallback callback = (format, _) =>
            {
                previewFormats.Add(format);
                return true;
            };

            NativeCapabilities.SupportedPreviewPixelFormats(_camera.GetHandle(), callback).
                ThrowIfFailed("Failed to get the supported preview formats.");

            return previewFormats.AsReadOnly();
        }

        private IList<CameraFps> GetSupportedPreviewFps()
        {
            var previewFps = new List<CameraFps>();

            NativeCapabilities.FpsCallback callback = (fps, _) =>
            {
                previewFps.Add(fps);
                return true;
            };

            NativeCapabilities.SupportedPreviewFps(_camera.GetHandle(), callback).
                ThrowIfFailed("Failed to get the supported camera fps");

            return previewFps.AsReadOnly();
        }

        private IList<CameraFps> GetSupportedPreviewFpsByResolutions(int width, int height)
        {
            var fpsByResolution = new List<CameraFps>();

            NativeCapabilities.FpsByResolutionCallback callback = (fps, _) =>
            {
                fpsByResolution.Add(fps);
                return true;
            };

            NativeCapabilities.SupportedPreviewFpsByResolution(_camera.GetHandle(), width, height, callback).
                ThrowIfFailed("Failed to get the supported fps by resolutions.");

            return fpsByResolution.AsReadOnly();
        }

        private IList<CameraAutoFocusMode> GetSupportedAutoFocusModes()
        {
            var autoFocusModes = new List<CameraAutoFocusMode>();

            NativeCapabilities.AfModeCallback callback = (mode, _) =>
            {
                autoFocusModes.Add(mode);
                return true;
            };

            NativeCapabilities.SupportedAutoFocusModes(_camera.GetHandle(), callback).
                ThrowIfFailed("Failed to get the supported Auto focus modes.");

            return autoFocusModes.AsReadOnly();
        }

        private IList<CameraExposureMode> GetSupportedExposureModes()
        {
            var exposureModes = new List<CameraExposureMode>();

            NativeCapabilities.ExposureModeCallback callback = (mode, _) =>
            {
                exposureModes.Add(mode);
                return true;
            };

            NativeCapabilities.SupportedExposureModes(_camera.GetHandle(), callback).
                ThrowIfFailed("Failed to get the supported Exposure modes.");

            return exposureModes.AsReadOnly();
        }

        private IList<CameraIsoLevel> GetSupportedIsoLevels()
        {
            var isoLevels = new List<CameraIsoLevel>();

            NativeCapabilities.IsoCallback callback = (iso, _) =>
            {
                isoLevels.Add(iso);
                return true;
            };

            NativeCapabilities.SupportedIso(_camera.GetHandle(), callback).
                ThrowIfFailed("Failed to get the supported Iso levels.");

            return isoLevels.AsReadOnly();
        }

        private IList<CameraTheaterMode> GetSupportedTheaterModes()
        {
            var theaterModes = new List<CameraTheaterMode>();

            NativeCapabilities.TheaterModeCallback callback = (theaterMode, _) =>
            {
                theaterModes.Add(theaterMode);
                return true;
            };

            NativeCapabilities.SupportedTheaterModes(_camera.GetHandle(), callback).
                ThrowIfFailed("Failed to get the supported theater modes.");

            return theaterModes.AsReadOnly();
        }

        private IList<CameraWhiteBalance> GetSupportedWhitebalances()
        {
            var whitebalances = new List<CameraWhiteBalance>();

            NativeCapabilities.WhitebalanceCallback callback = (whiteBalance, _) =>
            {
                whitebalances.Add(whiteBalance);
                return true;
            };

            NativeCapabilities.SupportedWhitebalance(_camera.GetHandle(), callback).
                ThrowIfFailed("Failed to get the supported white balance.");

            return whitebalances.AsReadOnly();
        }

        private IList<CameraFlashMode> GetSupportedFlashModes()
        {
            var flashModes = new List<CameraFlashMode>();

            NativeCapabilities.FlashModeCallback callback = (flashMode, _) =>
            {
                flashModes.Add(flashMode);
                return true;
            };

            NativeCapabilities.SupportedFlashModes(_camera.GetHandle(), callback).
                ThrowIfFailed("Failed to get the supported flash modes.");

            return flashModes.AsReadOnly();
        }

        private IList<CameraSceneMode> GetSupportedSceneModes()
        {
            var sceneModes = new List<CameraSceneMode>();

            NativeCapabilities.SceneModeCallback callback = (sceneMode, _) =>
            {
                sceneModes.Add(sceneMode);
                return true;
            };

            NativeCapabilities.SupportedSceneModes(_camera.GetHandle(), callback).
                ThrowIfFailed("Failed to get the supported scene modes.");

            return sceneModes.AsReadOnly();
        }

        private IList<CameraEffectMode> GetSupportedEffects()
        {
            var effectModes = new List<CameraEffectMode>();

            NativeCapabilities.EffectCallback callback = (effect, _) =>
            {
                effectModes.Add(effect);
                return true;
            };

            NativeCapabilities.SupportedEffects(_camera.GetHandle(), callback).
                ThrowIfFailed("Failed to get the supported camera effects.");

            return effectModes.AsReadOnly();
        }

        private IList<Rotation> GetSupportedStreamRotations()
        {
            var streamRotations = new List<Rotation>();

            NativeCapabilities.StreamRotationCallback callback = (streamRotation, _) =>
            {
                streamRotations.Add(streamRotation);
                return true;
            };

            NativeCapabilities.SupportedStreamRotations(_camera.GetHandle(), callback).
                ThrowIfFailed("Failed to get the supported camera rotations.");

            return streamRotations.AsReadOnly();
        }

        private IList<Flips> GetSupportedStreamFlips()
        {
            var streamFlips = new List<Flips>();

            NativeCapabilities.StreamFlipCallback callback = (streamFlip, _) =>
            {
                streamFlips.Add(streamFlip);
                return true;
            };

            NativeCapabilities.SupportedStreamFlips(_camera.GetHandle(), callback).
                ThrowIfFailed("Failed to get the supported camera flips.");

            return streamFlips.AsReadOnly();
        }

        private IList<CameraPtzType> GetSupportedPtzTypes()
        {
            var ptzTypes = new List<CameraPtzType>();

            NativeCapabilities.PtzTypeCallback callback = (ptzType, _) =>
            {
                ptzTypes.Add(ptzType);
                return true;
            };

            NativeCapabilities.SupportedPtzTypes(_camera.GetHandle(), callback).
                ThrowIfFailed("Failed to get the supported Ptz types.");

            return ptzTypes.AsReadOnly();
        }
        #endregion Methods for getting supported values
    }
}
