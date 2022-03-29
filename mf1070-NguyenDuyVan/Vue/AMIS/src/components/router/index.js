import { createRouter, createWebHistory } from "vue-router";
import EmployeeList from "../view/employee/EmployeeList";
import ComboBoxRecord from "../base/ComboBoxRecord";

const routes = [
  { path: "/employees", component: EmployeeList },
  { path: "/cash/deposit", component: ComboBoxRecord },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
