/*
 * Copyright(c) 2021 Samsung Electronics Co., Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
#pragma warning disable CS0162 // Unreachable code detected: Some lines can be unreachable in TV profile
namespace Tizen.NUI.Components
{
    internal partial class DefaultThemeCreator
    {
        private DefaultThemeCreator() { }

        public static IThemeCreator Instance { get; set; } = new DefaultThemeCreator();

        public static void Preload()
        {
            if (ThemeManager.InitialThemeDisabled) return;

            ThemeManager.AddPackageTheme(Instance);

            if (string.IsNullOrEmpty(ExternalThemeManager.CurrentThemeId)) return;

            ThemeManager.LoadPlatformTheme(ExternalThemeManager.CurrentThemeId);
        }
    }
}
#pragma warning restore CS0162
