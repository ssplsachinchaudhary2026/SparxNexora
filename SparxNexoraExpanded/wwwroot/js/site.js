import * as THREE from 'three';
const root=document.getElementById('webgl-bg');
if(root){
 const scene=new THREE.Scene(); const camera=new THREE.PerspectiveCamera(55,innerWidth/innerHeight,.1,100); camera.position.z=9;
 const renderer=new THREE.WebGLRenderer({antialias:true,alpha:true}); renderer.setPixelRatio(Math.min(devicePixelRatio,1.5)); renderer.setSize(innerWidth,innerHeight); root.appendChild(renderer.domElement);
 const group=new THREE.Group(); scene.add(group);
 const geo=new THREE.BufferGeometry(),count=800,positions=new Float32Array(count*3);
 for(let i=0;i<count;i++){positions[i*3]=(Math.random()-.5)*28;positions[i*3+1]=(Math.random()-.5)*17;positions[i*3+2]=(Math.random()-.5)*16} geo.setAttribute('position',new THREE.BufferAttribute(positions,3));
 group.add(new THREE.Points(geo,new THREE.PointsMaterial({color:0x4f8cff,size:.018,transparent:true,opacity:.75})));
 const torus=new THREE.Mesh(new THREE.TorusKnotGeometry(1.8,.035,150,10),new THREE.MeshBasicMaterial({color:0x00d9ff,wireframe:true,transparent:true,opacity:.15})); torus.position.set(4.4,.5,-2); group.add(torus);
 let mx=0,my=0; addEventListener('pointermove',e=>{mx=(e.clientX/innerWidth-.5)*2;my=(e.clientY/innerHeight-.5)*2});
 const reduced=matchMedia('(prefers-reduced-motion: reduce)').matches;
 function animate(){if(!reduced){requestAnimationFrame(animate);group.rotation.y+=.00035;group.rotation.x+=.00008;torus.rotation.x+=.0015;torus.rotation.y+=.001;group.position.x+=(mx*.25-group.position.x)*.02;group.position.y+=(-my*.15-group.position.y)*.02}renderer.render(scene,camera)} animate();
 addEventListener('resize',()=>{camera.aspect=innerWidth/innerHeight;camera.updateProjectionMatrix();renderer.setSize(innerWidth,innerHeight)})
}
const observer=new IntersectionObserver(entries=>entries.forEach(e=>e.isIntersecting&&e.target.classList.add('visible')),{threshold:.08}); document.querySelectorAll('.reveal').forEach(e=>observer.observe(e));
const menu=document.querySelector('.menu-btn'),nav=document.querySelector('#mainNav');menu?.addEventListener('click',()=>nav.classList.toggle('open'));
