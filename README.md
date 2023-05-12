# Unity Animator Layer Duplication
Editor extensions that allow you to create shallow copies of layers inside an Animator Controller

The scripts add two new options under the "Tools" contex menu - "Layer Duplicator" and "Duplicated Layer Deletion".

The Layer Duplicator windows lets you select an Animator Controller and a layer that's part of it. It then creates a shallow copy of that layer. A new layer with the same base state machine as the selected layer. This means that any changes made in either of the two layers would be reflected in the other one, INCLUDING the deletion of a layer.

This is useful when you want the same Animation Layer to use more than one Avatar mask, since as of making this, it is not possible to programmatically change the avatar mask assigned to an animation layer. With this sccript you can create unlimited shallow copies of a layer and assing a different Avatar Mask to each one. Then, in your code, you can specify which ones are active by setting their weight parameter to 0 or 1.

**IMPORTANT**

Since the duplicating layers are referencing the same objects and don't have copies of those objects, deleting a duplicated layer would also delete the original layer. This is why if you want to delete a previously duplicated layer, you need to use the other script "Duplicated Layer Deletion" which will safely delete only the duplicated layer without affecting the source layer or other duplicated layers.


### How to install

Copy the two scripts LayerDuplicator.cs and DeleteAnimatorLayer.cs into Assets/Editor. If there's no Editor folder in your Assets folder, create it. Once you put the scripts there, you can find the options in the Unity Editor menu bar under the "Tools" option.
