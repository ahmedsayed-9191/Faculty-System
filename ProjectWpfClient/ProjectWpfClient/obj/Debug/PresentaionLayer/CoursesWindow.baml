   M S B A M L   `   `   `  ����K  GProjectWpfClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:ProjectWpfClientProjectWpfClient  5   	   $    ProjectWpfClient.CoursesWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:ProjectWpfClient     5   	     � Title$  Courses��5   	   $��340.853��6   $��640.947��6*   
���iii� � WindowStartupLocation$ CenterScreen=�6O   .��5	      � $��0,0,15,8q�6   .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dataGridCourses��5
      $
��Left=�6,   $��	10,10,0,0q�6G   $	��Top=�6Z   $	��313��6r   $	��156��6~   �� 5   
   -   5         ��Name$ buttonShowAllCourses��5      $��arial)�6/   $
��10.5(�6B   $��	ExtraBold"�6R   $��Show All Courses��6j   $
��Left=�6�   $��
82,181,0,0q�6�   $	��Top=�6�   $	��126��6�   6�   �� 5   
   -   5        $ Close��5      $��arial)�6    $
��10.5(�63   $��	ExtraBold"�6C   $��Close
��6[   $
��Left=�6u   $��543,256,0,0q�6�   $	��Top=�6�   $��61��6�   $��36��6�   �� 5   
   -   5         ��Name$ textBoxCourseCode��5      $
��Left=�6-   $��23��6H   $��
455,55,0,0q�6T    �� TextWrapping$
 Wrap=�6h   $	��Top=�6|   $	��120��6�   �� 5   
   -   5        $ textBoxCourseDescription��5      $
��Left=�64   $��23��6O   $��455,100,0,0q�6[   $
 Wrap=�6p   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $ textBoxCourseId��5      $
��Left=�6+   $��23��6F   $��
472,12,0,0q�6R   $
 Wrap=�6f   $	��Top=�6z   $��43��6�   $y�Hidden=�6�   �� 5   
   -   5         ��Name$ labelDescription��5      
�������$��arial)�6B   $��12(�6U   $��	ExtraBold"�6c   $��Description :��6z   $
��Left=�6�   $��358,100,0,0q�6�   $	��Top=�6�    J� RenderTransformOrigin$ 0.526,-0.077&�6�   �� 5   
   -   5        $ 	labelCode��5      
�������$��arial)�6;   $��12(�6N   $��	ExtraBold"�6\   $��Code :��6s   $
��Left=�6�   $��
397,54,0,0q�6�   $	��Top=�6�   $ 0.526,-0.077&�6�   �� 5   
   -	   5        $ labelCourseData��5      
�������$��arial)�6A   $��12(�6T   $��	ExtraBold"�6b   $��Course's Data��6y   $
��Left=�6�   $��
375,15,0,0q�6�   $	��Top=�6�   $ 0.526,-0.077&�6�   �� 5   
   -
   5        $ buttonGetACourse��5      $��arial)�6+   $
��10.5(�6>   $��	ExtraBold"�6N   $��Ger A Course��6f   $
��Left=�6}   $��371,195,0,0q�6�   $	��Top=�6�   $��99��6�   $��36��6�   �� 5   
   -   5        $ textBoxCourseIdSearch��5      $
��Left=�61   $��23��6L   $��490,205,0,0q�6X   $
 Wrap=�6m   $	��Top=�6�   $��78��6�   $ 0.564,4.204&�6�   $��Course Id ?��6�   �� 5   
   -   5        $ buttonAddCourse��5      $��arial)�6+   $
��10.5(�6>   $��	ExtraBold"�6N   $��Add New Course��6e   $
��Left=�6~   $��330,143,0,0q�6�   $	��Top=�6�   $	��119��6�   $ 0.133,0.725&�6�   $��39��6  �� 5   
   -   5        $ buttonUpdate��5      $y�Visible=�6'   $��arial)�6<   $
��10.5(�6O   $��	ExtraBold"�6_   $��Update��6v   $
��Left=�6�   $��470,143,0,0q�6�   $	��Top=�6�   $��66��6�   $ 0.576,0.861&�6�   �� 5   
   -   5        $ buttonDelete��5      $y�Visible=�6'   $��arial)�6<   $
��10.5(�6O   $��	ExtraBold"�6_   $��Delete��6v   $
��Left=�6�   $��542,143,0,0q�6�   $	��Top=�6�   $��64��6�   $ 0.492,1.194&�6�   5      5      