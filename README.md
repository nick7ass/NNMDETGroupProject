 # Introduction
## Elemental Mysteries: Quest for the Elemental Essences

"Elemental Mysteries" is an educational Virtual Reality (VR) experience that lets you explore the ancient concepts of water, fire, air, and earth that were believed to form everything. This idea was introduced by Empedocles, a Greek pre-Socratic philosopher, and was the cornerstone of philosophy and science for two thousand years.
The problem detected was a growing disconnect from the natural world and an underappreciation of the rich cultural and historical insights that the study of the four elements offers. In an age where digital environments often replace physical experiences, the depth of understanding and the sense of wonder about the natural world has diminished. This detachment not only limits our perspective on the environment but also our ability to draw upon the wealth of knowledge and inspiration that historical and cultural interpretations of these elements provide.
The proposed solution, "Elemental Mysteries," is valuable because it bridges this gap, offering an immersive learning platform where history, science, philosophy, and art converge. Engaging users in an interactive 3D exploration of the four elements not only educates but also rekindles curiosity for the natural world. It encourages a deeper appreciation of how older cultures have understood and symbolized these elements, highlighting their significance in philosophy, psychology, and even astrology. Through "Elemental Mysteries," learners are not just absorbing information; they are invited to see the world through a lens that blends ancient wisdom with modern technology, fostering a connection that surpasses time.


## Design Process
The project concept stemmed from collaborative brainstorming sessions, where our goal was to select an interesting educational topic that has the potential to be explored in extended reality. After thorough consideration, we gravitated towards delving into the historical significance of the four elemental forces of water, fire, air, and earth. We began by sketching initial outlines on how to translate our vision into reality. To ensure complete coverage, we opted to envision distinct interactions for each element. Consequently, we sketched four unique stations, each dedicated to exploring the essence and influence of one of the elements from the beginning.
Initially, we crafted personas representing potential users interested in utilizing this application.
![Persona One.jpg](https://www.dropbox.com/scl/fi/9yz9vgrmf1txic44iy9ji/Persona-One.jpg?rlkey=laqpmr6azr0hh8w1e7dtgb3hg&dl=0&raw=1)
![Persona Two.jpg](https://www.dropbox.com/scl/fi/i9eq3t8oirv4mh1hory61/Persona-Two.jpg?rlkey=kfk181pnfypnast3tcofzvakv&dl=0&raw=1)
The planning phase of the project involved gathering historical information from reliable sources on the internet to ensure accuracy and authenticity. We then created a storyline that would guide users through the exploration of each element, incorporating historical and cultural insights. 
The design process for our project underwent several phases of ideation, testing, and refinement to create an immersive and coherent experience for users interacting with the elemental stations. Initially, our concept revolved around using distinct objects for each station, tailored to represent the unique characteristics of that element. However, we pivoted towards a more unified approach, designing four similar totems, each with specific elemental features. This decision not only enhanced the aesthetic cohesion of the scene but also simplified the user experience.
![Four Stations.png](https://www.dropbox.com/scl/fi/nbjbigr172edbz4hxpgqc/Four-Stations.png?rlkey=fx782a5qgus7pucwxm0vvh5h2&dl=0)
In the water element station, we transitioned from a concept featuring a waterfall to a bowl of water, better aligned with the totem's design and functionality.
![IMG_1702.jpg](https://www.dropbox.com/scl/fi/3rtgsi8tn6b7tsee67mov/IMG_1702.jpg?rlkey=45qiqtwb4snbvft1akqeaezjo&dl=0&raw=1)
For the tangible interaction of our project, we initially explored various sensor options to detect user interaction. Our initial consideration involved using a flex sensor with a moist cover, where users would physically touch the moistened surface to trigger the interaction. However, upon further evaluation, we pivoted towards using a distance sensor to detect user presence near the water element. As the project progressed and we decided to incorporate a bowl of water as part of the interaction, we opted for a more direct approach utilizing the ESP32-S2's built-in touch sensor functionality. Leveraging the GPIO pins that support touch sensing, we devised a solution where a cable was submerged into the water. When a user places their hand into the water, the touch sensor detects the change in conductivity, signaling the presence of the user's hand. A servo motor is also attached to a glass that pours some water on the user's hand when the touch is detected.
![Circuit for Touch Sensor.jpg](https://www.dropbox.com/scl/fi/43904qnkryon7nubr4hie/Circuit-for-Touch-Sensor.jpg?rlkey=7ct28gh66799flla8ax1rcc6t&dl=0)
![Touch Detection.jpg](https://www.dropbox.com/scl/fi/ku81qf19fcaf1ewvd52yf/Touch-Detection.jpg?rlkey=mh4rfld3d63llt513ubdyo1gt&dl=0)
Similarly, for the air element, we refined the initial breeze-like object and repositioned it beside the totem following user testing. Adjustments were made to ensure the breeze was positioned at an optimal distance to avoid causing discomfort to users. Initially, we considered implementing a motion sensor for interaction, but due to the potential for excessive hand movement by users, we explored an alternative solution within Unity. In this iteration, users can interact by pointing with their index finger and making a circular motion, causing the breeze to transform into a tornado-like effect.
The fire element station initially incorporated a campfire alongside firewood, intending for users to interact by throwing wood into the fire. However, after testing revealed ambiguity in user instructions, we refined the design. The campfire was removed, and a single piece of firewood was positioned on a pillar, enhancing clarity and ease of interaction. Moreover, the appearance of the firewood was synchronized with the narration, guiding users through the interaction process. The interaction here happens in VR, where users have to grab the wood and throw it into the fire.
![IMG_1703.jpg](https://www.dropbox.com/scl/fi/ptqww2unu03igacvcq84q/IMG_1703.jpg?rlkey=hds16kp5rp6m9a8f3tksxp39l&dl=0&raw=1)
For the earth element station, we initially utilized a flat plane with soil material but later introduced a bowl of sand/soil atop the totem, aligning with the overall design scheme. As part of the tangible interaction, we integrated a force sensor beneath a layer of sand that detects the force applied by the user and made the earth's elemental object to appear in VR. Throughout the design process, we iterated on the placement of elemental objects, ensuring they were easily accessible and visible to users.
![Force Sensor Circuit.jpg](https://www.dropbox.com/scl/fi/lgc8vcaz5fokxe9069ce2/Force-Sensor-Circuit.jpg?rlkey=i7ncjxi070eligh6spoklwi0k&dl=0)
![Force Detection.jpg](https://www.dropbox.com/scl/fi/qrj3dwt3csda0zuwvvxam/Force-Detection.jpg?rlkey=lkqq9j4dxmwd7hy3kad3yrrxi&dl=0)
![Force in Sand.jpg](https://www.dropbox.com/scl/fi/caykpb555h29n8qyytknp/Force-in-Sand.jpg?rlkey=rtxclsriq5q0l1pctgcwng4uj&dl=0)
Responding to user feedback, subtitles were added for narrations at each station, providing additional guidance and instructions. This iterative approach allowed us to refine and optimize the user experience, resulting in a more engaging and intuitive interaction journey for users exploring the elemental stations.
Aligning the real-world environment with the VR space presented a significant challenge, with misalignment posing a threat to immersion. Sensors were positioned to mirror their VR counterparts for this matter, mitigating potential disruptions to user experience. Additional challenges included integrating sensors to independently trigger narrations and interactions and ensuring seamless transitions between elements and stations within the VR environment. Moreover, addressing potential sensor malfunctions and user errors during interaction further complicated matters. To overcome these challenges, an iterative approach was adopted, involving testing and refinement of both code and hardware setups. 

## System description
### Features
- Immersive exploration of the four elements through detailed 3D models and environments.
- An ultimate virtual experience
- Hand-tracking: Enabling hand interactions such as grabbing and throwing 
- Hand Pose Detection with Oculus Interaction SDK
- Interactive tangible interactions using touch and force detection integrated with ESP32-S2
- Voice-over narration and storytelling, guiding users through the experience
- Integration with Unity using XR Plug-in Management 
- Oculus as the plug-in provider, with Quest 2, Quest 3, and Quest Pro as target devices
- Visual and audio feedback combined with affordances

For a demonstration of the project's functionality, please visit the following link:
[Demo Video](https://drive.google.com/file/d/1FNTVYRTObIoVDy56z3V__hkAS_pfEYow/view?usp=sharing)

## Installation

This section outlines the steps to set up your environment for developing Android VR applications using [Unity](https://unity.com/) 2022.3 or higher.
### Step One: Setting Up Unity Hub
1. **Download and install Unity Hub** from the [Unity download page](https://unity3d.com/get-unity/download).
2. Open Unity Hub after installation.
### Step Two: Installing Unity Editor and Required Modules
1. In Unity Hub, navigate to the 'Installs' tab and click on the 'Add' button to install a new version of the Unity Editor.
2. Select **Unity Editor LTS version 2022.3.Xf1** or higher from the list of available versions. You can find the recommended versions on the [Unity releases page](https://unity.com/releases/editor/whats-new/2022.3.10).
3. During the installation setup, ensure to include the following modules:
   - **Microsoft Visual Studio IDE** (for code editing).
   - **Android Build Support** (libraries necessary for creating Android
4. Follow the instructions to complete the Unity Editor installation.
### Configuring a Unity Project for Android VR
1. **Create a new Unity project** from the 'Projects' tab in Unity Hub. Choose the **3D (URP)** template, name your project, and select a location on your computer to save it.
2. **Switch the build platform to Android**:
   - Open your project in Unity, go to `File > Build Settings`, and select 'Android' as the target platform. Click on 'Switch Platform' to apply the change.
3. **Import Meta XR SDK**:
   - Go to `Window > Package Manager`, click on the '+' icon, and select 'Add package by name'. Enter `com.meta.xr.sdk.all` and click 'Add'. Restart Unity if prompted.
4. **Configure Unity for XR development**:
   - Access `Project Settings`, navigate to the 'Oculus' option in the left menu, and click on 'Fix All' to apply the necessary settings for VR development.
   - Under `Project Settings`, go to 'XR Plug-in Management' and ensure that 'Oculus' is checked for the Android platform.
### Step 4: Setup for Tangible Interaction Using ESP32-S2
To facilitate tangible interactions in your VR project, you will need to program an ESP32-S2 board and enable communication between it and Unity. This section guides you through setting up the Arduino IDE and configuring your project for WiFi communication.
Setting Up Arduino IDE for ESP32-S2
Download and Install the Arduino IDE: Download the [Arduino IDE](hhttps://www.arduino.cc/en/software) from the Arduino website and follow the installation instructions.

Add ESP32 Board to Arduino IDE:

Open the Arduino IDE, go to File > Preferences (on Windows) or Arduino > Preferences (on Mac).
In the "Additional Board Manager URLs" field, add the following URL: https://dl.espressif.com/dl/package_esp32_index.json then click "OK".
Go to Tools > Board > Boards Manager, search for "ESP32", and install the latest version of the "ESP32 by Espressif Systems" package.
Select Your ESP32-S2 Board:

Navigate to Tools > Board and select the appropriate ESP32 board from the list (e.g., ESP32-S2 Dev Module).
Connect and Configure Your ESP32-S2 Board:

With the ESP32-S2 board connected to your computer via USB, select the correct port under Tools > Port.
You may need to install drivers for your ESP32-S2 board if it's not automatically recognized by your computer. Refer to your board's documentation for driver installation instructions.
Programming ESP32-S2 for WiFi Communication
Write or Load the Sketch: Use the Arduino IDE to write or load a pre-existing sketch that enables WiFi functionality on your ESP32-S2. This sketch should handle communication between the ESP32-S2 and Unity via your local network.

Specify the IP Address in Your Unity Project:

Ensure your ESP32-S2 is connected to the same local network as your development machine.
In your Unity project, you'll need to specify the IP address of the ESP32-S2 board for network communication. This can be done within your scripts that handle the interaction between Unity and the ESP32-S2.


### Step 5: Building Your First XR App
1. With your project configured, navigate to `File > Build Settings`.
2. Ensure the Android platform is selected, and your scene is included in the build.
3. Click 'Build' and choose a name and location for the generated APK file.


## Usage
To interact with [Elemental Mysteries] and explore its features, follow the guidelines below:
- **Hand Interaction**: Use your hands to make poses, grab, throw, and interact with objects in the virtual environment.
- **Exploration**: Move around the environment by walking or using hand gestures to navigate through different stations.
- **Grabbing Elements**: Approach an elemental object closely and use your hand to grab it.
- **Narration**: Listen to the narration that guides you through each element's historical significance and how to interact with it.
- **Tangible Interaction**: Touch the objects mentioned by the narration to trigger an action.
- **Collecting Elemental Objects**: Touch the object that appears after your interaction with each station.

## References
### Educational Resources
[Understanding the 4 Elements & Using them to Shift Your Energy](https://www.cassieuhl.com/blog/understanding-the-4-elements-using-them-to-shift-your-energy#:~:text=energy%20body%20systems%3F-,The%20four%20elements%2C%20earth%2C%20air%2C%20fire%2C%20and%20water,to%20understand%20your%20own%20energy)
[What are the FOUR Elements? Science Lesson: Earth, Water, Air, and Fire](https://learning-center.homesciencetools.com/article/four-elements-science/)
[Empedocles](https://www.worldhistory.org/Empedocles/)
### Unity Assets
[Meta XR All-in-One SDK](https://assetstore.unity.com/packages/3d/environments/ancient-jungle-temple-demo-123179)
[Ancient Jungle Temple Demo](https://www.worldhistory.org/Empedocles/)
[Elemental Magic Totems](https://assetstore.unity.com/packages/3d/elemental-magic-totems-59522)
[Lowpoly Flowers](https://assetstore.unity.com/packages/3d/vegetation/plants/lowpoly-flowers-47083)
[Stylize Water Texture](https://assetstore.unity.com/packages/2d/textures-materials/water/stylize-water-texture-153577)
[The Free Medieval and War Props](https://assetstore.unity.com/packages/3d/props/the-free-medieval-and-war-props-174433)
[Particle Ribbon](https://assetstore.unity.com/packages/vfx/particles/spells/particle-ribbon-42866)
[TOOLKIT vol.2 Sound Pack Bundle](https://assetstore.unity.com/packages/audio/sound-fx/toolkit-vol-2-sound-pack-bundle-198011#description)
[Ambience nature Australia 05](https://freesound.org/people/busabx/sounds/715252/)
### Youtube Tutorials
[Hand Gesture Detection with Unity XR Hand Tracking](https://www.youtube.com/watch?v=Lc1PuEatrCA)
[HHow To Grab And Throw Objects In VR - Interaction SDK #4](https://www.youtube.com/watch?v=Ril-5dWBOSU&t=915s)

## Contributors
This project has been brought to life thanks to the hard work and dedication of the following individuals:

- **[Nicklas Bourelius](https://www.linkedin.com/in/nicklas-bourelius-1362a9225/)** 
- **[Negin Soltani](https://www.linkedin.com/in/negin-soltani-5764911b9/)** 
- **[Masoomeh Advand](https://www.linkedin.com/in/masoomeh-advand-3259442aa/)**

