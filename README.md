# Readme

## ShapeChange

继承自Singleton设置为单例模式，挂载一个到场景中，然后将需要变形的物体的skinmeshRenderer挂载上去即可

![image-20241123230513716](https://xmuzhensimage.oss-cn-hangzhou.aliyuncs.com/image/image-20241123230513716.png)

## SliderControl

挂载到slider UI组件上，然后设置相应要控制的blendshape的名字

比如这个就是控制鼻子的变大变小，变大变小情况取决于blendshape捏的效果，捏blendshape的时候可以分别捏个最大值和最小值，不然这个可能就只能调大或者调小

![image-20241123230653080](https://xmuzhensimage.oss-cn-hangzhou.aliyuncs.com/image/image-20241123230653080.png)
