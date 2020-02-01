Browsemonkey
============

Download latest Browsemonkey installer (Windows, requires .Net) : https://github.com/shukriadams/browsemonkey/releases/download/0.5.1/BrowseMonkey-0.5.1.exe

Browsemonkey takes snapshots of file systems for offline browsing. It was primarily intended to catalog offline storage media like CDRs, DVDRs, USB drives etc. Use it to browse and search for files on media that are not physically connected.


Build
-----
- Make sure you have MsBuild installed and in your system path.
- Run /build/build.bat, output will be in /_build


Status
------
It works. The old 3rd party docking and menu libraries were buggy and had to be removed. These will be rewritten when time permits, but the base application is functional again.


History
-------
Browsemonkey was written in 2004. The original project page was at http://sourceforge.net/projects/browsemonkey

It was my first open source software project, and also my first attempt to write a serious Windows program. After putting it on Sourceforge I drifted into web development. In 2014 I noticed that people were still downloading it from Sourceforge, but that internal changes to Windows had broken some parts of Browsemonkey that worked back in the days of .Net 2.0. So, I decided to move the project to Github and fix it. Browsemonkey is still not being actively developed, but hopefully with modern source control and CI it will be easier for me to tweak it iteratively.
