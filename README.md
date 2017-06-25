# PhotoAlbum
This is a set of helper tools which allow taking ilf full power of MS word to create photo albums.

## Basic functionnalities are:
* create set of documents from directory tree (one document per folder). Use PicturesSorter to select the relevant photos first!
* Automatically create low-resolution picture files to ease editing of document with a large number of pictures (up to ~100 photos per document is fine, with Word 2013/2016 64 bits and 16Gb of RAM)
* easy align, fit images
* Arrange on page with predefined patterns, fix margins and padding

# PicturesSorter
This is a bare winforms tool intending to visualize and compare two pictures at a time, and easily archive one of them (or both or none).
I created that on return from a wonderful vacation of 3 weeks in the National Parks in Utah, bringing back thousands of exciting photographs...

## Basic functionnalities are:
* pick directory
* display 2 pictures (jpg) from that directory
* navigate 1 picture forward or backward (using left / right keys)
* Move either of the pictures to Archive subfolder (using numpad 1 or 2 keys)

#TODO
* *done* Remove size group (redundant with alignments)
* *done* Arrange group on the left of the ribbon
* *done* Hi/Low res: "prepare doc for print" or "restore pictures resolution for print"
* Arrange: make margin padding subordinate!
* Arrange aligments: disable useless buttons (horiz diagonal when arranged on horiz line, for instance)
* Arrange: need a button for one image centering full page
* Arrange: keep last used button highlighted
* Arrange: discover selected images arrangement and highlight the right button
* globally: doc-to-ribbon feedback
* globally: tool tips and tool descriptions
