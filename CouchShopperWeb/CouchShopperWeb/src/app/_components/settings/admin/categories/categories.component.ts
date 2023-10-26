import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Category } from '../../../../_models/_common/request/category-request';
import { CommonService } from '../../../../_services/common.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {
  categoryForm !: FormGroup;
  showCategoryForm: boolean = false;
  categoryFormSubmitted: boolean = false;
  selectedCategory: Category | null = null;
  categories: Category[] = [];
  newCategory: string = '';
  newIcon: string = '';
  numberOfCategories = 0;
  categoryCurrentPage = 1;
  itemsPerPage = 5;
  maxSizePagination = 5;
  isDisabled: boolean =true;

  constructor(private formBuilder: FormBuilder, private commonService: CommonService) { }

  ngOnInit(): void {
    this.getCategories();
    this.categoryForm = this.formBuilder.group({
      name: ['', Validators.required],
    })
  }

  getCategories(): void {
    this.commonService.getCategories(this.categoryCurrentPage)
      .subscribe(
        {
          next: (response => {
            this.numberOfCategories = response.totalEntities;
            this.categories = response.categories;
          }),
          error: (
            error => {
              console.log(error.error.message)
            })
        }
      );
  }

  addCategory(): void {
    if (this.categoryForm.valid) {
      this.categoryFormSubmitted = false;
      const category: Category = {
        name: this.newCategory,
        iconName: 'fas ' + this.newIcon
      }
      this.commonService.addCategory(category).subscribe(
        {
          next: (response => {
            this.getCategories()
            this.newCategory = '';
            this.newIcon = '';
            this.isDisabled = true;
          }),
          error: (
            error => {
              console.log(error.error.message)
            })
        }
      );
    }
    else {
      this.categoryFormSubmitted = true
    }
  }

  deleteCategory(categoryId: string): void {
    this.commonService.deleteCategory(categoryId)
      .subscribe(
        {
          next: (response => {
            this.getCategories()
          }),
          error: (
            error => {
              console.log(error.error.message)
            })
        }
      );
  }

  editCategory(category: Category): void {
    this.selectedCategory = category;
  }

  saveCategory(category: Category): void {
    if (this.categoryForm.valid) {
      
      this.commonService.updateCategory(category).subscribe(
        {
          next: (response => {
            this.getCategories()
          }),
          error: (
            error => {
              console.log(error.error.message)
            })
        }
      );
    }
   
    this.selectedCategory = null;
  }

  cancelEditCategory(): void {
    this.selectedCategory = null;
  }

  categoryPageChanged(event: any): void {
    this.categoryCurrentPage = event.page;
    this.getCategories();
  }

  onIconSelected(iconName: string) {
    this.newIcon = iconName;
    this.isDisabled=this.newIcon===''
  }
  

}
