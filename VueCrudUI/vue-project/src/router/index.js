import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import EmployeeView from '../views/Employees/View.vue'
import EmployeeCreate from '../views/Employees/Create.vue'
import EmployeeEdit from '../views/Employees/Edit.vue'
  
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../views/AboutView.vue')
    },
    {
      path: '/employees',
      name: 'employees',
      component: EmployeeView
    },
    {
      path: '/employees/create',
      name: 'employeecreate',
      component: EmployeeCreate
    },
    {
      path: '/employees/edit/:id',
      name: 'employeeedit',
      component: EmployeeEdit
    }
  ]
})

export default router
