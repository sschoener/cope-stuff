﻿/*
Copyright (c) 2011 Sebastian Schoener

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 */
namespace RBFPlugin
{
    public static class RBFSettings
    {
        static RBFEditorPlugin s_singleton;

        public static RBFEditorPlugin Instance
        {
            get { return s_singleton; }
            set { s_singleton = value; }
        }

        #region settings

        public static bool AutoReloadInTestMode
        {
            get { return s_singleton.AutoReloadInTestMode; }
            set { s_singleton.AutoReloadInTestMode = value; }
        }

        public static bool UseKeyProviderForLoading
        {
            get { return s_singleton.UseKeyProviderForLoading; }
            set { s_singleton.UseKeyProviderForLoading = value; }
        }

        public static bool UseKeyProviderForSaving
        {
            get { return s_singleton.UseKeyProviderForSaving; }
            set { s_singleton.UseKeyProviderForSaving = value; }
        }

        public static bool UseAutoCompletion
        {
            get { return s_singleton.UseAutoCompletion; }
            set { s_singleton.UseAutoCompletion = value; }
        }

        #endregion settings
    }
}